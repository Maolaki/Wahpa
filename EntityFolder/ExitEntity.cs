namespace EntityEngine
{
    internal class ExitEntity : EntityTemplate
    {
        ExitEntity() : base(false, 0f)
        { }

        public override void Invoke()
        {
            WindowEngine.ViewHandler.ChangeRoom();
        }
    }
}
