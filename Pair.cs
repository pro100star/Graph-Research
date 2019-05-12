namespace WF {
    public struct Pair<T, U> {
        public Pair(T first, U second) {
            First = first;
            Second = second;
        }

        public T First;
        public U Second;

        public override string ToString() {
            return $"{First} {Second}";
        }
    }
}
