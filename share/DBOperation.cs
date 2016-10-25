using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace LYH.WorkOrder.share
{
    /// <summary>
    /// 类名：DBOperation
    /// 作用：创建连接字符串，打开、关闭、释放连接，执行存储过程，
    /// </summary>
    public class DbOperation : IDisposable
    {
        private SqlConnection _con;
        private const string Returnvalue = "RETURNVALUE";
        //记录错误日志位置
        private static string _mFileName = "D:\\Systemlog.txt";

        #region 创建连接字符串
        /// <summary>
        /// CreateCon创建连接字符串
        /// </summary>
        /// <returns></returns>
        public SqlConnection CreateCon()
        {
            var con = new SqlConnection("server=61.129.47.15;database=taobao; uid=taobao;pwd=870104");
            //或者使用如下方式，调用webConfig的连接字符串
            //string strcon = ConfigurationManager.ConnectionStrings["yhqhjnConnectionString"].ConnectionString;
            //SqlConnection con = new SqlConnection(strcon);
            return con;
        }

        #endregion
    
        #region 打开数据库连接.
        private void Open()
        {
            // 打开数据库连接
            if (_con == null)
            {
                _con = CreateCon();
            }

            if (_con.State == ConnectionState.Closed)
            {
                try
                {
                    ///打开数据库连接
                    _con.Open();
                }
                catch (Exception ex)
                {
                    CreateErrorMsg(ex.Message);
                }
                finally
                {
                    ///关闭已经打开的数据库连接		
                    _con.Close();
                }
            }
        }
        #endregion

        #region 关闭数据库连接
        public void Close()
        {
            ///判断连接是否已经创建
            if (_con != null)
            {
                ///判断连接的状态是否打开
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
            }
        }
        #endregion
   
        #region 释放资源
        public void Dispose()
        {
            // 确认连接是否已经关闭
            if (_con != null)
            {
                _con.Dispose();
                _con = null;
            }
        }
        #endregion
    
        #region 执行无参存储过程
        /// <summary>
        /// 执行存储过程,执行添加、删除、更新类型操作
        /// </summary>
        public int RunProc(string procName)
        {
            var cmd = CreateProcCommand(procName, null);
            ///执行存储过程
            cmd.ExecuteNonQuery();
            ///关闭数据库的连接
            Close();

            ///返回存储过程的参数值
            return (int)cmd.Parameters[Returnvalue].Value;
        }
        #endregion
    
        #region 执行有参存储过程
        /// <summary>
        /// 执行存储过程，执行添加、删除、更新类型操作
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数列表</param>
        /// <returns></returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            var cmd = CreateProcCommand(procName, prams);
            ///执行存储过程
            cmd.ExecuteNonQuery();
            ///关闭数据库的连接
            Close();

            ///返回存储过程的参数值
            return (int)cmd.Parameters[Returnvalue].Value;
        }
        #endregion
    
        #region 执行无参存储过程，返回datareader对象
        /// <summary>
        /// 执行存储过程，执行选择类型操作
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dataReader">要返回的DataReader对象</param>
        public void RunProc(string procName, out SqlDataReader dataReader)
        {
            ///创建Command
            var cmd = CreateProcCommand(procName, null);

            ///读取数据
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region 执行有参存储过程，返回datareader对象
        /// <summary>
        /// 执行存储过程，执行选择类型操作
        /// </summary>
        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            ///创建Command
            var cmd = CreateProcCommand(procName, prams);

            ///读取数据
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion
    
        #region 执行无参存储过程，返回DataSet对象
        /// <summary>
        /// 执行无参存储过程，返回DataSet对象
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dataSet"></param>
        public void RunProc(string procName, ref DataSet dataSet)
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
            }
            ///创建SqlDataAdapter
            var da = CreateProcDataAdapter(procName, null);

            ///读取数据
            da.Fill(dataSet);
            ///关闭数据库的连接
            Close();
        }

        #endregion
    
        #region 执行有参存储过程，返回DataSet对象
        /// <summary>
        /// 执行有参存储过程，返回DataSet对象
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数列表</param>
        /// <param name="dataSet">返回对象</param>
        public void RunProc(string procName, SqlParameter[] prams, ref DataSet dataSet)
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
            }
            ///创建SqlDataAdapter
            var da = CreateProcDataAdapter(procName, prams);

            ///读取数据
            da.Fill(dataSet);
            ///关闭数据库的连接
            Close();
        }
        #endregion    

        #region 创建一个SqlCommand对象以此来执行存储过程
        /// <summary>
        /// 创建一个SqlCommand对象以此来执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <returns></returns>

        private SqlCommand CreateProcCommand(string procName, SqlParameter[] prams)
        {
            Open();
            ///设置Command
            var cmd = new SqlCommand(procName, _con) { CommandType = CommandType.StoredProcedure };

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (var parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            ///添加返回参数ReturnValue
            cmd.Parameters.Add(
                new SqlParameter(Returnvalue, SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlCommand对象
            return cmd;
        }	

        #endregion

        #region 创建一个SqlDataAdapter对象，用此来执行存储过程
        /// <summary>
        /// 创建一个SqlDataAdapter对象，用此来执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数列表</param>
        /// <returns></returns>
        private SqlDataAdapter CreateProcDataAdapter(string procName, SqlParameter[] prams)
        {
            ///打开数据库连接
            Open();

            ///设置SqlDataAdapter对象
            var da = new SqlDataAdapter(procName, _con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (var parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }

            ///添加返回参数ReturnValue
            da.SelectCommand.Parameters.Add(
                new SqlParameter(Returnvalue, SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlDataAdapter对象
            return da;
        }
        #endregion
    
        #region 生成存储过程参数
        /// <summary>
        /// 生成存储过程参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="direction">形式参数类型</param>
        /// <param name="value">形参名称</param>
        /// <returns></returns>
        public SqlParameter CreateParam(string paramName, SqlDbType dbType, Int32 size, ParameterDirection direction, object value)
        {
            SqlParameter param = null;

            ///当参数大小为0时，不使用该参数大小值
            if (size > 0)
            {
                param = new SqlParameter(paramName, dbType, size);
            }
            else
            {
                ///当参数大小为0时，不使用该参数大小值
                param = new SqlParameter(paramName, dbType);
            }

            ///创建输出类型的参数
            param.Direction = direction;
            if (!(direction == ParameterDirection.Output && value == null))
            {
                param.Value = value;
            }
            ///返回创建的参数
            return param;
        }
        #endregion
    
        #region 输入传入参数
        /// <summary>
        /// 输入传入参数和
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public SqlParameter CreateInParam(string paramName, SqlDbType dbType, int size, object value)
        {
            return CreateParam(paramName, dbType, size, ParameterDirection.Input, value);
        }
        #endregion

        #region 传入返回值参数
        /// <summary>
        /// 传入返回值参数
        /// </summary>
        public SqlParameter CreateOutParam(string paramName, SqlDbType dbType, int size)
        {
            return CreateParam(paramName, dbType, size, ParameterDirection.Output, null);
        }
        #endregion

        #region 传入返回值参数
        /// <summary>
        /// 传入返回值参数
        /// </summary>
        public SqlParameter CreateReturnParam(string paramName, SqlDbType dbType, int size)
        {
            return CreateParam(paramName, dbType, size, ParameterDirection.ReturnValue, null);
        }
        #endregion

        #region 将DataReader转为DataTable
        /// <summary>
        /// 将DataReader转为DataTable
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataTable ConvertdrTodt(SqlDataReader dataReader)
        {
            ///定义DataTable
            var datatable = new DataTable();

            ///动态添加表的数据列
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                var mydc = new DataColumn() { DataType = dataReader.GetFieldType(i), ColumnName = dataReader.GetName(i) };
                datatable.Columns.Add(mydc);
            }

            ///添加表的数据
            while (dataReader.Read())
            {
                var mydr = datatable.NewRow();
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    mydr[i] = dataReader[i].ToString();
                }
                datatable.Rows.Add(mydr);
                mydr = null;
            }
            ///关闭数据读取器
            dataReader.Close();
            return datatable;
        }

        #endregion

        #region 记录日志至文本文件
        /// <summary>
        /// 记录日志至文本文件
        /// </summary>
        /// <param name="message">记录的内容</param>
        public static void CreateErrorMsg(string message)
        {
            if (File.Exists(_mFileName))
            {
                ///如果日志文件已经存在，则直接写入日志文件
                var sr = File.AppendText(_mFileName);
                sr.WriteLine("\n");
                sr.WriteLine(DateTime.Now + message);
                sr.Close();
            }
            else
            {
                ///创建日志文件
                var sr = File.CreateText(_mFileName);
                sr.Close();
            }	
        }
        #endregion
    
    }
}
