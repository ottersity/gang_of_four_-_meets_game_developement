using System;
using Components;
using Scripts.Components;

namespace Pattern.Visitor
{
    public interface IVisitor
    {

        void Visit(HealthComponent element);
        void Visit(AttackComponent element);
        void Visit(VisionComponent element);

    }
}