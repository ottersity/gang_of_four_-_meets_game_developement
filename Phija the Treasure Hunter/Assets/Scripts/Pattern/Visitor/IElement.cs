namespace Pattern.Visitor
{
    public interface IElement
    {

        public void Accept(IVisitor visitor);
    }
}