namespace DatabaseExample
{
    public delegate void EventHandler<in T1, in T2>(T1 sender, T2 args);
}
