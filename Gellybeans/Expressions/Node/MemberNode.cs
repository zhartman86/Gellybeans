﻿using System.Text;

namespace Gellybeans.Expressions
{
    public class MemberNode : ExpressionNode
    {
        readonly ExpressionNode parent;
        readonly VarNode member;
        

        public MemberNode(ExpressionNode parent, VarNode member)
        {
            this.parent = parent;
            this.member = member;         
        }       
        
        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            var value = parent.Eval(depth, caller, sb, ctx);

            if(value is IMember m && m.TryGetMember(member.VarName, out var outVal))
                return outVal;

            else return "%";
        }
    }
}
