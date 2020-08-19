using LYH.Framework.ControlUtil;
using WorkOrder.ProcCard.Entity;

namespace WorkOrder.ProcCard.BLL
{
	/// <summary>
	/// 员工管理
	/// </summary>
	public class ProcCardEmployee : BaseBLL<ProcCardEmployeeInfo>
	{
		public ProcCardEmployee() : base()
		{
			base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
		}
	}
}
