using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Utls
{
    public class NpoiHelper
    {
        /// <summary>
        /// Excel文件导成Datatable
        /// </summary>
        /// <param name="strFilePath">Excel文件目录地址</param>
        /// <param name="strTableName">Datatable表名</param>
        /// <param name="iSheetIndex">Excel sheet index</param>
        /// <returns></returns>
        public static DataTable XlSToDataTable(string strFilePath, string strTableName, int iSheetIndex)
        {
            string strExtName = Path.GetExtension(strFilePath).ToLower();

            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(strTableName))
            {
                dt.TableName = strTableName;
            }

            IWorkbook workbook = null;

            using (FileStream file = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
            {
                if (strExtName.Equals(".xls"))
                    workbook = new HSSFWorkbook(file);
                else if (strExtName.Equals(".xlsx"))
                    workbook = new XSSFWorkbook(file);
                else
                    throw new NotSupportedException("不支持的格式文件");

                ISheet sheet = workbook.GetSheetAt(iSheetIndex);

                //列头
                foreach (ICell item in sheet.GetRow(1).Cells)
                {
                    dt.Columns.Add(item.ToString(), typeof(string));
                }

                //写入内容
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    IRow row = (IRow)rows.Current;
                    if (row.RowNum <= 1)//sheet.FirstRowNum)
                        continue;

                    DataRow dr = dt.NewRow();
                    foreach (ICell item in row.Cells)
                    {
                        switch (item.CellType)
                        {
                            case CellType.Boolean:
                                dr[item.ColumnIndex] = item.BooleanCellValue;
                                break;
                            case CellType.Error:
                                //dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                break;
                            case CellType.Formula:
                                switch (item.CachedFormulaResultType)
                                {
                                    case CellType.Boolean:
                                        dr[item.ColumnIndex] = item.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        //dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(item))
                                        {
                                            dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = item.NumericCellValue;
                                        }
                                        break;
                                    case CellType.String:
                                        string str = item.StringCellValue;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            dr[item.ColumnIndex] = str.ToString();
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = null;
                                        }
                                        break;
                                    case CellType.Unknown:
                                    case CellType.Blank:
                                    default:
                                        dr[item.ColumnIndex] = string.Empty;
                                        break;
                                }
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(item))
                                {
                                    dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                }
                                else
                                {
                                    dr[item.ColumnIndex] = item.NumericCellValue;
                                }
                                break;
                            case CellType.String:
                                string strValue = item.StringCellValue;
                                if (!string.IsNullOrEmpty(strValue))
                                {
                                    dr[item.ColumnIndex] = strValue;
                                }
                                else
                                {
                                    dr[item.ColumnIndex] = null;
                                }
                                break;
                            case CellType.Unknown:
                            case CellType.Blank:
                            default:
                                dr[item.ColumnIndex] = string.Empty;
                                break;
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
    }
}
