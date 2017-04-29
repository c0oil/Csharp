using System.Collections.Generic;

namespace RaObjects.Objects
{
    public static class RiskAggregationMoves
    {
        private static readonly Dictionary<string, MoveInfo> moves = new Dictionary<string, MoveInfo>();


        public static void Clear()
        {
            moves.Clear();
        }

        public static bool IsMoved(int inVersion, string id)
        {
            MoveInfo moveInfo;
            return moves.TryGetValue(id, out moveInfo) && moveInfo.IsMoved(inVersion);
        }

        public static void RegisterMoves(string fromId, string toId, int changesInVersion)
        {
            if (!moves.ContainsKey(fromId))
            {
                moves.Add(fromId, new MoveInfo(toId, changesInVersion));
            }
        }

        private class MoveInfo
        {
            private readonly int changesInVersion;
            private string toId;

            public MoveInfo(string toId, int changesInVersion)
            {
                this.changesInVersion = changesInVersion;
                this.toId = toId;
            }

            public bool IsMoved(int inVersion)
            {
                return inVersion >= changesInVersion;
            }
        }
    }
}