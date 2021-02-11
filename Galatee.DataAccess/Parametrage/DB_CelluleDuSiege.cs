﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Galatee.Structure;
using Galatee.Entity.Model;

namespace Galatee.DataAccess
{
    public class DB_CelluleDuSiege /*: Galatee.DataAccess.Parametrage.DbBase*/
    {
        /*
        private string ConnectionString;

        public DB_CelluleDuSiege()
        {
           try
            {
                ConnectionString = Session.GetSqlConnexionString();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public DB_CelluleDuSiege(string ConnStr)
        {
            ConnectionString = ConnStr;
        }

        private SqlConnection cn = null;

        private bool _Transaction;

        public bool Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }

        }

        private SqlCommand cmd = null;

        public List<CsCelluleDuSiege> SelectAllCelluleDuSiege()
        {
            cn = new SqlConnection(ConnectionString);
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cmd = new SqlCommand
                          {
                              Connection = cn,
                              CommandType = CommandType.StoredProcedure,
                              CommandText = EnumProcedureStockee.SelectCELLULEDUSIEGE
                          };
                IDataReader reader = cmd.ExecuteReader();
                var rows = new List<CsCelluleDuSiege>();
                Fill(reader, rows, int.MinValue, int.MaxValue);
                reader.Close();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception(EnumProcedureStockee.SelectCELLULEDUSIEGE + ":" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close(); // Fermeture de la connection 
                cmd.Dispose();
            }
        }

        public bool Delete(CsCelluleDuSiege pCelluleDuSiege)
        {
            try
            {
                cn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = EnumProcedureStockee.DeleteCELLULEDUSIEGE
                };
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CODE", pCelluleDuSiege.CODE);
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                StartTransaction(cn);
                int rowsAffected = cmd.ExecuteNonQuery();
                CommitTransaction(cmd.Transaction);
                return Convert.ToBoolean(rowsAffected);
            }
            catch (Exception ex)
            {
                RollBackTransaction(cmd.Transaction);
                throw new Exception(EnumProcedureStockee.DeleteCELLULEDUSIEGE + ":" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                cn.Close();
                cmd.Dispose();
            }
        }

        public bool Delete(List<CsCelluleDuSiege> pCelluleDuSiegeCollection)
        {
            int number = 0;
            foreach (CsCelluleDuSiege entity in pCelluleDuSiegeCollection)
            {
                if (Delete(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        public static List<CsCelluleDuSiege> Fill(IDataReader reader, List<CsCelluleDuSiege> rows, int start, int pageLength)
		{
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (! reader.Read() )
					return rows; // not enough rows, just return
			}

			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done

                var c = new CsCelluleDuSiege();
				c.CODE = (Convert.IsDBNull(reader["CODE"]))?string.Empty:(System.String)reader["CODE"];
				c.OriginalCODE = (Convert.IsDBNull(reader["CODE"]))?string.Empty:(System.String)reader["CODE"];
                c.LIBELLE = (Convert.IsDBNull(reader["LIBELLE"])) ? string.Empty : (System.String)reader["LIBELLE"];
                c.USERCREATION = (Convert.IsDBNull(reader["USERCREATION"])) ? string.Empty : (System.String)reader["USERCREATION"];
                c.USERMODIFICATION = (Convert.IsDBNull(reader["USERMODIFICATION"])) ? string.Empty : (System.String)reader["USERMODIFICATION"];
                c.DATECREATION = (Convert.IsDBNull(reader["DATECREATION"])) ? (DateTime?)null : (System.DateTime)reader["DATECREATION"];
                c.DATEMODIFICATION = (Convert.IsDBNull(reader["DATEMODIFICATION"])) ? (DateTime?)null : (System.DateTime)reader["DATEMODIFICATION"];
				rows.Add(c);
			}
			return rows;
		}

        public bool Update(CsCelluleDuSiege pCelluleDuSiege)
        {
                cn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand
                          {
                              Connection = cn,
                              CommandType = CommandType.StoredProcedure,
                              CommandText = EnumProcedureStockee.UpdateCELLULEDUSIEGE
                          };
                cmd.Parameters.Clear();

                try
                {
                    cmd.Parameters.AddWithValue("@CODE", pCelluleDuSiege.CODE);
                    cmd.Parameters.AddWithValue("@LIBELLE", pCelluleDuSiege.LIBELLE);
                    cmd.Parameters.AddWithValue("@OriginalCODE", pCelluleDuSiege.OriginalCODE);
                    cmd.Parameters.AddWithValue("@DATECREATION", pCelluleDuSiege.DATECREATION);
                    cmd.Parameters.AddWithValue("@DATEMODIFICATION", pCelluleDuSiege.DATEMODIFICATION);
                    cmd.Parameters.AddWithValue("@USERCREATION", pCelluleDuSiege.USERCREATION);
                    cmd.Parameters.AddWithValue("@USERMODIFICATION", pCelluleDuSiege.USERMODIFICATION);
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    StartTransaction(cn);
                    SetDBNullParametre(cmd.Parameters);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    CommitTransaction(cmd.Transaction);
                    return Convert.ToBoolean(rowsAffected);
                }
                catch (Exception ex)
                {
                    RollBackTransaction(cmd.Transaction);
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close(); // Fermeture de la connection 
                    cmd.Dispose();
                }
        }

        public bool Update(List<CsCelluleDuSiege> pCsCelluleDuSiegeCollection)
        {
            int number = 0;
            foreach (CsCelluleDuSiege entity in pCsCelluleDuSiegeCollection)
            {
                if (Update(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        public bool Insert(CsCelluleDuSiege pCelluleDuSiege)
        {
                cn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = EnumProcedureStockee.InsertCELLULEDUSIEGE
                };
                cmd.Parameters.Clear();

                try
                {
                    cmd.Parameters.AddWithValue("@CODE", pCelluleDuSiege.CODE);
                    cmd.Parameters.AddWithValue("@LIBELLE", pCelluleDuSiege.LIBELLE);
                    cmd.Parameters.AddWithValue("@DATECREATION", pCelluleDuSiege.DATECREATION);
                    cmd.Parameters.AddWithValue("@DATEMODIFICATION", pCelluleDuSiege.DATEMODIFICATION);
                    cmd.Parameters.AddWithValue("@USERCREATION", pCelluleDuSiege.USERCREATION);
                    cmd.Parameters.AddWithValue("@USERMODIFICATION", pCelluleDuSiege.USERMODIFICATION);
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    StartTransaction(cn);

                    SetDBNullParametre(cmd.Parameters);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    CommitTransaction(cmd.Transaction);
                    return Convert.ToBoolean(rowsAffected);
                }
                catch (Exception ex)
                {
                    RollBackTransaction(cmd.Transaction);
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close(); // Fermeture de la connection 
                    cmd.Dispose();
                }
        }

        public bool Insert(List<CsCelluleDuSiege> pCelluleDuSiegeCollection)
        {
            int number = 0;
            foreach (CsCelluleDuSiege entity in pCelluleDuSiegeCollection)
            {
                if (Insert(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        private void StartTransaction(SqlConnection _conn)
        {
            if ((_Transaction) && (_conn != null))
            {
                cmd.Transaction = this.BeginTransaction(_conn);
            }
        }

        private void CommitTransaction(SqlTransaction _pSqlTransaction)
        {
            if ((_Transaction) && (_pSqlTransaction != null))
            {
                this.Commit(_pSqlTransaction);
            }
        }

        private void RollBackTransaction(SqlTransaction _pSqlTransaction)
        {
            if ((_Transaction) && (_pSqlTransaction != null))
            {
                this.RollBack(_pSqlTransaction);
            }
        }

        */

        public List<CsCelluleDuSiege> SelectAllCelluleDuSiege()
        {
            try
            {
                return Entities.GetEntityListFromQuery<CsCelluleDuSiege>(ParamProcedure.PARAM_CELLULEDUSIEGE_RETOURNE());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(CsCelluleDuSiege pCelluleDuSiege)
        {
            try
            {
                return Entities.DeleteEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiege));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<CsCelluleDuSiege> pCelluleDuSiegeCollection)
        {
            try
            {
                return Entities.DeleteEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiegeCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(CsCelluleDuSiege pCelluleDuSiege)
        {
            try
            {
                return Entities.UpdateEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiege));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(List<CsCelluleDuSiege> pCelluleDuSiegeCollection)
        {
            try
            {
                return Entities.UpdateEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiegeCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CsCelluleDuSiege pCelluleDuSiege)
        {
            try
            {
                return Entities.InsertEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiege));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(List<CsCelluleDuSiege> pCelluleDuSiegeCollection)
        {
            try
            {
                return Entities.InsertEntity<Galatee.Entity.Model.CELLULEDUSIEGE>(Entities.ConvertObject<Galatee.Entity.Model.CELLULEDUSIEGE, CsCelluleDuSiege>(pCelluleDuSiegeCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
