   public class DynamicArrayEventArgs : EventArgs
    {
        public int OldCapacity { get; }
        public int NewCapacity { get; }
        public DynamicArrayEventArgs(int oldCapacity, int newCapacity)
        {
            OldCapacity = oldCapacity;
            NewCapacity = newCapacity;
        }
    }
