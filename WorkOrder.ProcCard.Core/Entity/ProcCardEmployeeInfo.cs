using System.Runtime.Serialization;
using LYH.Framework.ControlUtil;

namespace WorkOrder.ProcCard.Entity
{
	/// <summary>
	/// 员工管理
	/// </summary>
	[DataContract]
	public class ProcCardEmployeeInfo : BaseEntity
	{ 
		/// <summary>
		/// 默认构造函数（需要初始化属性的在此处理）
		/// </summary>
		public ProcCardEmployeeInfo()
		{
			this.ID= 0;
			 this.TeamId= 0;
	
		}

		#region Property Members
		
		[DataMember]
		public virtual int ID { get; set; }

		/// <summary>
		/// 班组
		/// </summary>
		[DataMember]
		public virtual int TeamId { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		[DataMember]
		public virtual string Name { get; set; }

		/// <summary>
		/// 代号
		/// </summary>
		[DataMember]
		public virtual string Code { get; set; }


		#endregion

	}
}