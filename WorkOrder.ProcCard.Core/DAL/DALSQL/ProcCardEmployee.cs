using System.Collections;
using System.Data;
using System.Collections.Generic;
using LYH.Framework.Commons;
using LYH.Framework.ControlUtil;
using WorkOrder.ProcCard.Entity;
using WorkOrder.ProcCard.IDAL;

namespace WorkOrder.ProcCard.DALSQL
{
	/// <summary>
	/// 员工管理
	/// </summary>
	public class ProcCardEmployee : BaseDALSQL<ProcCardEmployeeInfo>, IProcCardEmployee
	{
		#region 对象实例及构造函数

		public static ProcCardEmployee Instance
		{
			get
			{
				return new ProcCardEmployee();
			}
		}
		public ProcCardEmployee() : base("PD_ProcCard_Employee","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ProcCardEmployeeInfo DataReaderToEntity(IDataReader dataReader)
		{
			ProcCardEmployeeInfo info = new ProcCardEmployeeInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.TeamId = reader.GetInt32("TeamId");
			info.Name = reader.GetString("Name");
			info.Code = reader.GetString("Code");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(ProcCardEmployeeInfo obj)
		{
			ProcCardEmployeeInfo info = obj as ProcCardEmployeeInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("TeamId", info.TeamId);
			hash.Add("Name", info.Name);
			hash.Add("Code", info.Code);
				
			return hash;
		}

		/// <summary>
		/// 获取字段中文别名（用于界面显示）的字典集合
		/// </summary>
		/// <returns></returns>
		public override Dictionary<string, string> GetColumnNameAlias()
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			#region 添加别名解析
			//dict.Add("ID", "编号");
			 dict.Add("TeamId", "班组");
			 dict.Add("Name", "姓名");
			 dict.Add("Code", "代号");
			 #endregion

			return dict;
		}

	}
}