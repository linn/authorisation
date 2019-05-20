namespace Linn.Authorisation.Domain
{
    public class Permission
    {
        public Permission(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
