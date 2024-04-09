using System;
using System.Text;
using Gellybeans.Expressions;
using Gellybeans.Pathfinder;

namespace Gellybeans.Expressions
{
    public class FunctionNode : ExpressionNode
    {
        readonly string functionName;
        readonly ExpressionNode[] args;

        readonly static Random rand = new();

        public FunctionNode(string functionName, ExpressionNode[] args = null!)
        {
            this.functionName = functionName;
            this.args = args;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";


            dynamic[] argValues = null!;
            if (args != null)
            {
                argValues = new dynamic[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    argValues[i] = args[i].Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
                }

            }

            return Call(functionName, depth, caller, argValues, ctx, sb);
        }

        public dynamic Call(string functionName, int depth, object caller, dynamic[] args = null!, IContext ctx = null!, StringBuilder sb = null!) => functionName switch
        {
            "abs" => Math.Abs(args[0]),
            "clamp" => Math.Clamp(args[0], args[1], args[2]),
            "max" => Math.Max(args[0], args[1]),
            "min" => Math.Min(args[0], args[1]),
            "mod" => Math.Max(-5, args[0] >= 10 ? (args[0] - 10) / 2 : (args[0] - 11) / 2),
            "rand" => rand.Next(args[0], args[1] + 1),
            "bad" => args[0] / 3,
            "good" => 2 + args[0] / 2,
            "tq" => (args[0] + args[0] / 2) / 2,
            "oh" => args[0] / 2,
            "th" => args[0] + args[0] / 2,
            "upper" => args[0].ToString().ToUpper(),
            "lower" => args[0].ToString().ToLower(),
            "print" => Print(args[0], depth, caller, ctx, sb),
            "shuffle" => Shuffle(args[0]),
            "sumdec" => SumDecimal(args),
            "get_item" => GetItem(args[0]),
            "get_init" => GetInit(args[0]),
            "get_feat" => GetFeat(args[0]),
            "get_trait" => GetTrait(args[0]),
            _ => 0
        };


        static ArrayValue GetFeat(dynamic nameOrIndex)
        {
            nameOrIndex = nameOrIndex.ToString();

            int index = 0;
            if(nameOrIndex != "")
            {
                if(int.TryParse(nameOrIndex, out int outVal) && outVal >= 0 && outVal < Feat.Feats.Count)
                    index = outVal;
                else
                    index = Feat.Feats.FindIndex(x => x.Name!.ToUpper() == nameOrIndex.ToUpper())!;
            }

            return Feat.Feats[index].ToArrayValue();
        }

        static ArrayValue GetItem(dynamic nameOrIndex)
        {
            nameOrIndex = nameOrIndex.ToString();
            
            int index = 0;
            if(nameOrIndex != "")
            {
                if(int.TryParse(nameOrIndex, out int outVal) && outVal >= 0 && outVal < Item.Items.Count)
                    index = outVal;
                else
                    index = Item.Items.FindIndex(x => x.Name!.ToUpper() == nameOrIndex.ToUpper())!;
            }

            return Item.Items[index].ToArrayValue();
        }

        static ArrayValue GetInit(dynamic nameOrIndex)
        {
            nameOrIndex = nameOrIndex.ToString();
            int index = 0;
            if(nameOrIndex != "")
            {
                if(int.TryParse(nameOrIndex, out int outVal) && outVal >= 0 && outVal < Creature.Creatures.Count)
                    index = outVal;
                else
                    index = Creature.Creatures.FindIndex(x => x.Name!.ToUpper() == nameOrIndex.ToUpper())!;
            }

            return Creature.Creatures[index].ToInit();

        }

        static ArrayValue GetTrait(dynamic nameOrIndex)
        {
            nameOrIndex = nameOrIndex.ToString();
            int index = 0;
            if(nameOrIndex != "")
            {
                if(int.TryParse(nameOrIndex, out int outVal) && outVal >= 0 && outVal < Trait.Traits.Count)
                    index = outVal;
                else
                    index = Trait.Traits.FindIndex(x => x.Name!.ToUpper() == nameOrIndex.ToUpper())!;
            }

            return Trait.Traits[index].ToArrayValue();
        }

        static string SumDecimal(dynamic[] args)
        {
            decimal sum = 0;
            for(int i  = 0; i < args.Length; i++)
                if(decimal.TryParse(args[i].ToString(), out decimal result))
                    sum += result;
            
            return sum.ToString();
        }

        static string Print(dynamic s, int depth, object caller, IContext ctx, StringBuilder sb)
        {
            if(sb != null)
            {
                var sv = new StringValue(s.ToString()).Display(depth, caller, sb, ctx);
                sb.AppendLine(sv);
            }
            
            return "";
        }


        static ArrayValue Shuffle(ArrayValue array)
        {

            int n = array.Values.Length;
            dynamic[] a = new dynamic[n];
            array.Values.CopyTo(a, 0);
            while (n > 1)
            {
                int k = rand.Next(n--);
                (a[k], a[n]) = (a[n], a[k]);
            }
            return new ArrayValue(a);
        }


    }
}
