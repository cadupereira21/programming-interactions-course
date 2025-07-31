namespace GameInput.Command {
    public abstract class Command<T, TU> {
        public abstract void Execute(T input, TU gameObject);
    }
}