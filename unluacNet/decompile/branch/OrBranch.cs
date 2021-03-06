namespace unluac.decompile.branch
{

	using Registers = unluac.decompile.Registers;
	using BinaryExpression = unluac.decompile.expression.BinaryExpression;
	using Expression = unluac.decompile.expression.Expression;

	public class OrBranch : Branch
	{

	  private readonly Branch left;
	  private readonly Branch right;

	  public OrBranch(Branch left, Branch right) : base(right.line, right.begin, right.end)
	  {
		this.left = left;
		this.right = right;
	  }

	  public override Branch invert()
	  {
		return new AndBranch(left.invert(), right.invert());
	  }

//  
//  @Override
//  public Branch invert() {
//    return new NotBranch(new AndBranch(left.invert(), right.invert()));
//  }
//  

	  public override int getRegister()
	  {
		int rleft = left.getRegister();
		int rright = right.getRegister();
		return rleft == rright ? rleft : -1;
	  }

	  public override Expression asExpression(Registers r)
	  {
		return new BinaryExpression("or", left.asExpression(r), right.asExpression(r), Expression.PRECEDENCE_OR, Expression.ASSOCIATIVITY_NONE);
	  }

	  public override void useExpression(Expression expression)
	  {
		left.useExpression(expression);
		right.useExpression(expression);
	  }


	}

}