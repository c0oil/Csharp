using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    public static class RiskAggregationIdNavigate
    {
        public static IIdNavigate GetById(this IIdNavigate item, RaId id)
        {
            if (item == null)
            {
                return null;
            }
            if (item.Id != null && item.Id.Equals(id))
            {
                return item;
            }

            IIdNavigate subItem = item.GetSubItem(id);
            return subItem?.GetById(id);
        }

        private static IIdNavigate GetSubItem(this IIdNavigate item, RaId id)
        {
            IIdNavigate subItem;
            if (item.SubItems == null)
            {
                subItem = null;
            }
            else if (item.SubItems.Count() == 1)
            {
                subItem = item.SubItems.First();
            }
            else
            {
                subItem = item.SubItems.FirstOrDefault(x => x.Id != null && x.Id.Contain(id));
            }
            return subItem;
        }

        public static bool Rename(this IIdNavigate data, string itemId, string newValue)
        {
            var item = data.GetById(itemId);
            if (item != null)
            {
                Debug.WriteLine($"Renamed {item.Name} to {newValue}");
                item.Name = newValue;
            }
            return item != null;
        }

        #region Move

        public static bool AddNewMovedItem<T>(this IIdNavigate data, string itemId, string name)
            where T : class, IMovedSubItems<T>, new()
        {
            var item = data.GetById(new RaId(itemId).GetPrevId()) as T;
            if (item == null)
            {
                return false;
            }

            item.MovedSubItems.Add(new T {Name = name, Id = itemId});
            return true;
        }

        public static bool MoveWithSubItems<T>(this IIdNavigate data, string itemId, string newItemId, int changesInVersion)
            where T : class, IMovedSubItems<T>, IIdNavigate
        {
            var moved = data.GetById(itemId) as T;
            var parentForMoved = data.GetByIdMoved<T>(new RaId(newItemId).GetPrevId()) as T;
            if (moved == null || parentForMoved == null)
            {
                return false;
            }

            var copyMoved = MoveWithSubItems(moved, newItemId, changesInVersion);
            parentForMoved.MovedSubItems.Add(copyMoved);
            return true;
        }

        private static T MoveWithSubItems<T>(T moved, string newItemId, int changesInVersion) where T : IMovedSubItems<T>
        {
            T copyMoved = Clone(moved, newItemId, changesInVersion);
            foreach (T subItem in moved.SubItems)
            {
                string newSubItemId = subItem.Id.Id.Replace(moved.Id.Id, newItemId);
                copyMoved.MovedSubItems.Add(MoveWithSubItems(subItem, newSubItemId, changesInVersion));
            }

            return copyMoved;
        }

        public static bool Move<T>(this IIdNavigate data, string itemId, string newItemId, int changesInVersion)
            where T : class, IMovedSubItems<T>, IIdNavigate
        {
            var moved = data.GetById(itemId) as T;
            var parentForMoved = data.GetByIdMoved<T>(new RaId(newItemId).GetPrevId()) as T;
            if (moved == null || parentForMoved == null)
            {
                return false;
            }

            T copyMoved = Clone(moved, newItemId, changesInVersion);
            parentForMoved.MovedSubItems.Add(copyMoved);
            return true;
        }

        private static IIdNavigate GetByIdMoved<T>(this IIdNavigate item, RaId id) where T : IIdNavigate
        {
            if (item == null)
            {
                return null;
            }
            if (item.Id != null && item.Id.Equals(id))
            {
                return item;
            }

            IIdNavigate subItem = item.GetSubItem(id);
            if (subItem == null && item is IMovedSubItems<T>)
            {
                subItem = ((IMovedSubItems<T>)item).MovedSubItems.FirstOrDefault(x => x.Id != null && x.Id.Contain(id));
            }
            return subItem?.GetByIdMoved<T>(id);
        }

        private static T Clone<T>(T moved, string newItemId, int version) where T : IMovedSubItems<T>
        {
            RiskAggregationMoves.RegisterMoves(moved.Id.Id, newItemId, version);

            T copyMoved = Clone(moved);
            copyMoved.Id = newItemId;
            copyMoved.SubItems.Clear();
            return copyMoved;
        }

        private static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }

        #endregion

        public static bool ChangeId(this IIdNavigate data, string itemId, string newValue)
        {
            var item = data.GetById(itemId);
            if (item != null)
            {
                Debug.WriteLine($"Change Id {item.Id} to {newValue}");
                item.Id = newValue;
            }
            return item != null;
        }
    }
}