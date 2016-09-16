using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Oracle.DataAccess.Client;


namespace is2hinagata
{
	/// <summary>
	/// [is2hinagata]
	/// </summary>
	//--------------------------------------------------------------------------
	// �C������
	//--------------------------------------------------------------------------
	// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j��
	//	disposeReader(reader);
	//	reader = null;
	// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
	//	logFileOpen(sUser);
	//	userCheck2(conn2, sUser);
	//	logFileClose();
	//--------------------------------------------------------------------------
	// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX 
	//--------------------------------------------------------------------------

	[System.Web.Services.WebService(
		 Namespace="http://Walkthrough/XmlWebServices/",
		 Description="is2hinagata")]

	public class Service1 : is2common.CommService
	{
		public Service1()
		{
			//CODEGEN: ���̌Ăяo���́AASP.NET Web �T�[�r�X �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			connectService();
		}

		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		
		//Web �T�[�r�X �f�U�C�i�ŕK�v�ł��B
		private IContainer components = null;
				
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		/*********************************************************************
		* ���^�ꗗ�̎擾
		* �����F����b�c�A����b�c
		* �ߒl�F�X�e�[�^�X�A�����A���^�m�n�A���^���́A�t�@�C����
		**********************************************************************/
		[WebMethod]
		public String[] Get_hinagata(string[] sUser, String sKey1, String sKey2)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�ꗗ�擾�J�n");

// MOD 2005.05.11 ���s�j���� ORA-03113�΍�H START
//			string[] sRet = new string[1];
			OracleConnection conn2 = null;
			ArrayList sList = new ArrayList();
			string[] sRet = new string[2];
// MOD 2005.05.11 ���s�j���� ORA-03113�΍�H END

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet[0] = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			string cmdQuery = "";
			string sCnt = "0";
			int    iCnt = 0;
			try
			{
// MOD 2005.05.11 ���s�j���� ORA-03113�΍�H START
//				cmdQuery
////					= "SELECT TO_CHAR(COUNT(*)) \n"
////					= "SELECT NVL(COUNT(*),0) \n"
//					= "SELECT COUNT(*) \n"
//					+   "FROM �r�l�P�P�o�א��^ \n"
//					+  "WHERE ����b�c = '" + sKey1 + "' \n"
//					+    "AND ����b�c = '" + sKey2 + "' \n"
//					+    "AND �폜�e�f = '0' \n";
//
//				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);
//
//				if (reader.Read())
//				{
////					sCnt = reader.GetString(0);
//					sCnt = reader.GetDecimal(0).ToString().Trim();
//					iCnt = int.Parse(sCnt);
//				}
//
//				sRet = new string[2 + iCnt * 4];
//				sRet[1] = sCnt;
//				int iPos = 2;
//
//				if(iCnt > 0)
//				{ 
//					cmdQuery
//						= "SELECT ���^����, TO_CHAR(���^�m�n), �t�@�C����, \n"
//						+        "TO_CHAR(�X�V����) \n"
//						+   "FROM �r�l�P�P�o�א��^ \n"
//						+  "WHERE ����b�c = '" + sKey1 + "' \n"
//						+    "AND ����b�c = '" + sKey2 + "' \n"
//						+    "AND �폜�e�f = '0' \n"
//						+  "ORDER BY ���^�m�n \n";
//
//					reader = CmdSelect(sUser, conn2, cmdQuery);
//
//					while (reader.Read())
//					{
//						sRet[iPos++] = reader.GetString(0).Trim();
//						sRet[iPos++] = reader.GetString(1);
//						sRet[iPos++] = reader.GetString(2).Trim();
//						sRet[iPos++] = reader.GetString(3);
//						if(iPos >= sRet.Length) break;
//					}
//				}
//
//				sRet[0] = "����I��";

				cmdQuery
					= "SELECT ���^����, ���^�m�n, �t�@�C����, �X�V���� \n"
					+ " FROM �r�l�P�P�o�א��^ \n"
					+ " WHERE ����b�c = '" + sKey1 + "' \n"
					+ " AND ����b�c = '" + sKey2 + "' \n"
					+ " AND �폜�e�f = '0' \n"
					+ " ORDER BY ���^�m�n \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				while (reader.Read())
				{
					sList.Add(reader.GetString(0).Trim());
					sList.Add(reader.GetDecimal(1).ToString().Trim());
					sList.Add(reader.GetString(2).Trim());
					sList.Add(reader.GetDecimal(3).ToString().Trim());
					iCnt++;
				}
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END

				sRet = new string[2 + iCnt * 4];
				sCnt = (iCnt).ToString().Trim();

				if(sList.Count == 0)
				{
					sRet[0] = "";
					sRet[1] = "0";
				}
				else
				{
					iCnt = 2;
					IEnumerator enumList = sList.GetEnumerator();
					while(enumList.MoveNext())
					{
						sRet[iCnt] = enumList.Current.ToString();
						iCnt++;
					}
					sRet[0] = "����I��";
					sRet[1] = sCnt;
				}
// MOD 2005.05.11 ���s�j���� ORA-03113�΍�H END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* ���^�̍폜
		* �����F����b�c�A����b�c�A���^�m�n�A�X�V�����A�X�V�o�f�A�X�V��
		* �ߒl�F�X�e�[�^�X
		**********************************************************************/
		[WebMethod]
		public String Del_hinagata(string[] sUser, String[] sKey)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�폜�J�n");

			OracleConnection conn2 = null;
			string sRet = "";

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet = userCheck2(conn2, sUser);
//			if(sRet.Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			string sQuery = "";
			OracleTransaction tran;
			tran = conn2.BeginTransaction();
			try
			{
				sQuery
					= "UPDATE �r�l�P�P�o�א��^ \n"
					+    "SET �폜�e�f = '1', \n"
					+        "�X�V���� = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
					+        "�X�V�o�f = '" + sKey[4] + "', \n"
					+        "�X�V��   = '" + sKey[5] + "' \n"
					+  "WHERE ����b�c = '" + sKey[0] + "' \n"
					+    "AND ����b�c = '" + sKey[1] + "' \n"
					+    "AND ���^�m�n =  " + sKey[2] + " \n"
					+    "AND �폜�e�f = '0' \n"
					+    "AND �X�V���� =  " + sKey[3] + " \n";

				int iUpdRow = CmdUpdate(sUser, conn2, sQuery);

				tran.Commit();
				if(iUpdRow == 0)
					sRet = "�r���G���[�F���̒[���Ŋ��ɏC������Ă��܂���";
				else				
					sRet = "����I��";

				logWriter(sUser, INF, sRet);
			}
			catch (OracleException ex)
			{
				tran.Rollback();
				sRet = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				tran.Rollback();
				sRet = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* ���^�f�[�^�̎擾
		* �����F����b�c�A����b�c�A���^�m�n
		* �ߒl�F�X�e�[�^�X�A���^�̍���
		**********************************************************************/
		[WebMethod]
		public String[] Get_Hinagata2(string[] sUser, String sKey1, String sKey2, int iKey3)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�f�[�^�擾�J�n");

// MOD 2005.06.01 ���s�j�ɉ� �w����敪�ǉ� START
// MOD 2005.05.13 ���s�j�����J �ב��l�d�ʒǉ� START
//			string[] sRet = new string[36];
// MOD 2005.05.17 ���s�j�����J �ː��ǉ� START
//			string[] sRet = new string[37];
//			string[] sRet = new string[39];
			OracleConnection conn2 = null;
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
//			string[] sRet = new string[41];
			string[] sRet = new string[44];
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
// MOD 2005.05.17 ���s�j�����J �ː��ǉ� END
// MOD 2005.05.13 ���s�j�����J �ב��l�d�ʒǉ� END
// MOD 2005.06.01 ���s�j�ɉ� �w����敪�ǉ� END

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet[0] = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			string cmdQuery = "";
			try
			{
				cmdQuery
					= "SELECT "
					+ " S.���^����  , S.�t�@�C����, S.�׎�l�b�c   , S.�d�b�ԍ��P, \n"
					+ " S.�d�b�ԍ��Q, S.�d�b�ԍ��R, S.�Z���P       , S.�Z���Q    , \n"
					+ " S.�Z���R    , S.���O�P    , S.���O�Q       , SUBSTR(S.�X�֔ԍ�,1,3), \n"
					+ " SUBSTR(S.�X�֔ԍ�,4,4), S.�ב��l�b�c, S.�ב��l������ , TO_CHAR(S.��), \n"
					+ " TO_CHAR(S.�d��), S.�A���w���P, S.�A���w���Q, S.�i���L���P, \n"
					+ " S.�i���L���Q, S.�i���L���R, TO_CHAR(S.�ی����z), TO_CHAR(S.�X�V����), \n"
					+ " NVL(N.�d�b�ԍ��P,' '), NVL(N.�d�b�ԍ��Q,' '), NVL(N.�d�b�ԍ��R,' '), \n"
					+ " NVL(N.�Z���P,' ')    , NVL(N.�Z���Q,' ')    , NVL(N.���O�P,' '), \n"
					+ " NVL(N.���O�Q,' ')    , SUBSTR(NVL(N.�X�֔ԍ�,'   '),1,3), \n"
					+ " SUBSTR(NVL(N.�X�֔ԍ�,'       '),4,4), \n"
// MOD 2005.05.13 ���s�j�����J �ב��l�d�ʒǉ� START
//					+ " NVL(N.���Ӑ�b�c,' '), NVL(N.���Ӑ敔�ۂb�c,' ') \n"
					+ " NVL(N.���Ӑ�b�c,' '), NVL(N.���Ӑ敔�ۂb�c,' '),TO_CHAR(NVL(N.�d��,'0')), \n"
// MOD 2005.05.13 ���s�j�����J �ב��l�d�ʒǉ� END
// ADD 2005.05.17 ���s�j�����J �ː��ǉ� START
					+ " TO_CHAR(NVL(S.�ː�,'0')),TO_CHAR(NVL(N.�ː�,'0')) \n"
// ADD 2005.05.17 ���s�j�����J �ː��ǉ� END
// ADD 2005.06.01 ���s�j�ɉ� �A�����i�R�[�h�ǉ� START
					+ ",S.�A���w���b�c�P,S.�A���w���b�c�Q \n"
// ADD 2005.06.01 ���s�j�ɉ� �A�����i�R�[�h�ǉ� END
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
					+ ", S.�i���L���S, S.�i���L���T, S.�i���L���U \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
					+ "  FROM �r�l�P�P�o�א��^ S, \n"
					+ "       �r�l�O�P�ב��l   N \n"
					+ " WHERE S.����b�c = '" + sKey1 + "' \n"
					+   " AND S.����b�c = '" + sKey2 + "' \n"
					+   " AND S.���^�m�n =  " + iKey3
					+   " AND S.�폜�e�f = '0' \n"
					+   " AND '"+ sKey1 +"' = N.����b�c(+) \n"
					+   " AND '"+ sKey2 +"' = N.����b�c(+) \n"
					+   " AND S.�ב��l�b�c  = N.�ב��l�b�c(+) \n"
					+   " AND '0'           = N.�폜�e�f(+) \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				if (reader.Read())
				{
					for(int iCnt = 1; iCnt < sRet.Length; iCnt++)
					{
						sRet[iCnt] = reader.GetString(iCnt - 1).TrimEnd();
					}
					sRet[0] = "����I��";
				}
				else
				{
					sRet[0] = "�Y���f�[�^������܂���";
				}
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* ���^�f�[�^�̍X�V
		* �����F...
		* �ߒl�F�X�e�[�^�X
		**********************************************************************/
		[WebMethod]
		public String[] Upd_hinagata(string[] sUser, string[] sData)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�f�[�^�X�V�J�n");

			OracleConnection conn2 = null;
			string[] sRet = new string[5];

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet[0] = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			OracleTransaction tran;
			tran = conn2.BeginTransaction();

// �ב��l�b�c���݃`�F�b�N�̕ύX Start
//			string s���Ӑ�b�c     = " ";
//			string s���Ӑ敔�ۂb�c = " ";
//			string s���Ӑ敔�ۖ�   = " ";
//			string s����v         = " ";
//			try
//			{
//				//�ב��l�b�c���݃`�F�b�N�i���Ӑ���̎擾�j
//				string cmdQuery
//					= "SELECT N.���Ӑ�b�c, N.���Ӑ敔�ۂb�c, S.���Ӑ敔�ۖ� "
//					+  "FROM �b�l�O�Q���� B, "
//					+       "�r�l�O�P�ב��l N, "
//					+       "�r�l�O�S������ S "
//					+ "WHERE B.����b�c   = '" + sData[0]  +"' "
//					+   "AND B.����b�c   = '" + sData[1]  +"' "
//					+   "AND B.�폜�e�f   = '0' "
//					+   "AND N.����b�c   = '" + sData[0]  +"' "
//					+   "AND N.����b�c   = '" + sData[1]  +"' "
//					+   "AND N.�ב��l�b�c = '" + sData[17] +"' "
//					+   "AND N.�폜�e�f   = '0' "
//					+   "AND B.�X�֔ԍ�       = S.�X�֔ԍ� "
//					+   "AND N.���Ӑ�b�c     = S.���Ӑ�b�c "
//					+   "AND N.���Ӑ敔�ۂb�c = S.���Ӑ敔�ۂb�c "
//					;
//
//				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);
//
//				if( !reader.Read() ){
//					sRet[0] = "0";
//				}
//				else
//				{
//					s���Ӑ�b�c     = reader.GetString(0);
//					s���Ӑ敔�ۂb�c = reader.GetString(1);
//					s���Ӑ敔�ۖ�   = reader.GetString(2);

			decimal d����;
			string s����v         = " ";
			try
			{
				//�ב��l�b�c���݃`�F�b�N�i���Ӑ���̎擾�j
				string cmdQuery
//					= "SELECT NVL(COUNT(*),0) FROM �r�l�O�P�ב��l \n"
//					= "SELECT COUNT(*) FROM �r�l�O�P�ב��l \n"
					= "SELECT COUNT(ROWID) FROM �r�l�O�P�ב��l \n"
					+  "WHERE ����b�c   = '" + sData[0]  + "' \n"
					+    "AND ����b�c   = '" + sData[1]  + "' \n"
					+    "AND �ב��l�b�c = '" + sData[17] + "' \n"
					+    "AND �폜�e�f   = '0' \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				reader.Read();
				d����   = reader.GetDecimal(0);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
				if(d���� == 0)
				{
					sRet[0] = "0";
				}
				else
				{
// �ב��l�b�c���݃`�F�b�N�̕ύX End

					//����v�擾
					if( sData[6] != " " )
					{
						cmdQuery
							= "SELECT NVL(����v,' ') \n"
							+  "FROM �r�l�O�Q�׎�l \n"
							+ "WHERE ����b�c   = '" + sData[0] +"' \n"
							+   "AND ����b�c   = '" + sData[1] +"' \n"
							+   "AND �׎�l�b�c = '" + sData[6] +"' \n"
							+   "AND �폜�e�f   = '0' \n";

						reader = CmdSelect(sUser, conn2, cmdQuery);

						bool bRead = reader.Read();
						if(bRead == true)
							s����v   = reader.GetString(0);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
						disposeReader(reader);
						reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
					}

					int iPos = 4;
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
					string s�i���L���S = (sData.Length > 32) ? sData[32] : " ";
					string s�i���L���T = (sData.Length > 33) ? sData[33] : " ";
					string s�i���L���U = (sData.Length > 34) ? sData[34] : " ";
					if(s�i���L���S.Length == 0) s�i���L���S = " ";
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
					cmdQuery 
						= "UPDATE �r�l�P�P�o�א��^ \n"
						+    "SET ���^����   = '" + sData[iPos++] + "', \n"
						+        "�t�@�C���� = '" + sData[iPos++] + "', "
						+        "�׎�l�b�c = '" + sData[iPos++] + "', "
						+        "�d�b�ԍ��P = '" + sData[iPos++] + "', "
						+        "�d�b�ԍ��Q = '" + sData[iPos++] + "', \n"
						+        "�d�b�ԍ��R = '" + sData[iPos++] + "', "
						+        "�Z���P     = '" + sData[iPos++] + "', "
						+        "�Z���Q     = '" + sData[iPos++] + "', "
						+        "�Z���R     = '" + sData[iPos++] + "', \n"
						+        "���O�P     = '" + sData[iPos++] + "', "
						+        "���O�Q     = '" + sData[iPos++] + "', "
						+        "�X�֔ԍ�   = '" + sData[iPos++] + sData[iPos++] + "', "
						+        "����v     = '" + s����v   + "', \n"
						+        "�ב��l�b�c = '" + sData[iPos++] + "', "
						+        "�ב��l������ = '" + sData[iPos++] + "', "
						+        "��       =  " + sData[iPos++] + ", "
// ADD 2005.05.17 ���s�j�����J �ː��ǉ� START
						+        "�ː�       =  " + sData[29] + ", \n"
// ADD 2005.05.17 ���s�j�����J �ː��ǉ� END
						+        "�d��       =  " + sData[iPos++] + ", \n"
// ADD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� START
						+        "�A���w���b�c�P = '" + sData[30] + "', "
// ADD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� END
						+        "�A���w���P = '" + sData[iPos++] + "', "
// ADD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� START
						+        "�A���w���b�c�Q = '" + sData[31] + "', "
// ADD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� END
						+        "�A���w���Q = '" + sData[iPos++] + "', "
						+        "�i���L���P = '" + sData[iPos++] + "', "
						+        "�i���L���Q = '" + sData[iPos++] + "', \n"
						+        "�i���L���R = '" + sData[iPos++] + "', "
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
						+        "�i���L���S = '" +s�i���L���S+ "', "
						+        "�i���L���T = '" +s�i���L���T+ "', "
						+        "�i���L���U = '" +s�i���L���U+ "', \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
						+        "�ی����z   =  " + sData[iPos++] + ", "
						+        "�X�V�o�f   = '" + sData[iPos++] + "',"
						+        "�X�V��     = '" + sData[iPos++] + "',\n"
						+        "�X�V����   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') \n"
						+ " WHERE ����b�c   = '" + sData[0] + "' \n"
						+    "AND ����b�c   = '" + sData[1] + "' \n"
						+    "AND ���^�m�n   =  " + sData[2] + " \n"
						+    "AND �X�V����   =  " + sData[3] + " \n";

					int i�X�V���� = CmdUpdate(sUser, conn2, cmdQuery);

					// ����}�X�^�̐��^�m�n�̍X�V
					// �����F����b�c�A����b�c�A���^�m�n�A�X�V�o�f�A�X�V��
					int iRet = Set_hinagataNo(sUser, conn2, new string[]{sData[0],sData[1],sData[2],sData[27],sData[28]});

					tran.Commit();
					if(i�X�V���� == 0)
						sRet[0] = "�r���G���[�F���̒[���Ŋ��ɏC������Ă��܂���";
					else				
						sRet[0] = "����I��";
				}

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				tran.Rollback();
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				tran.Rollback();
				sRet[0] = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* ���^�f�[�^�̒ǉ�
		* �����F...
		* �ߒl�F�X�e�[�^�X
		**********************************************************************/
		[WebMethod]
		public String[] Ins_hinagata(string[] sUser, string[] sData)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�f�[�^�ǉ��J�n");

			OracleConnection conn2 = null;
			string[] sRet = new string[5];

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet[0] = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			OracleTransaction tran;
			tran = conn2.BeginTransaction();

			decimal d����;
			string s����v = " ";
			string cmdQuery = "";
			try
			{
				//�ב��l�b�c���݃`�F�b�N
				cmdQuery
//					= "SELECT NVL(COUNT(*),0) FROM �r�l�O�P�ב��l \n"
//					= "SELECT COUNT(*) FROM �r�l�O�P�ב��l \n"
					= "SELECT COUNT(ROWID) FROM �r�l�O�P�ב��l \n"
					+  "WHERE ����b�c   = '" + sData[0]  + "' \n"
					+    "AND ����b�c   = '" + sData[1]  + "' \n"
					+    "AND �ב��l�b�c = '" + sData[17] + "' \n"
					+    "AND �폜�e�f   = '0' \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				reader.Read();
				d����   = reader.GetDecimal(0);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
				if(d���� == 0)
				{
					sRet[0] = "0";
				}
				else
				{
					//����v�擾
					if(sData[6] != " ")
					{
						cmdQuery
							= "SELECT NVL(����v,' ') FROM �r�l�O�Q�׎�l \n"
							+  "WHERE ����b�c   = '" + sData[0] + "' \n"
							+    "AND ����b�c   = '" + sData[1] + "' \n"
							+    "AND �׎�l�b�c = '" + sData[6] + "' \n"
							+    "AND �폜�e�f   = '0' \n";

						reader = CmdSelect(sUser, conn2, cmdQuery);

						bool bRead = reader.Read();
						if(bRead == true)
							s����v   = reader.GetString(0);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
						disposeReader(reader);
						reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
					}

// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX START
//					// �����L�[�ō폜�t���O�����Ă����ꍇ�ɂ͍폜
//					cmdQuery 
//						= "DELETE FROM �r�l�P�P�o�א��^ \n"
//						+ " WHERE ����b�c = '" + sData[0] + "' \n"
//						+   " AND ����b�c = '" + sData[1] + "' \n"
//						+   " AND ���^�m�n =  " + sData[2] + " \n"
//						+   " AND �폜�e�f = '1' \n";
//
//					CmdUpdate(sUser, conn2, cmdQuery);
					string s�폜�e�f = "";
					cmdQuery 
						= "SELECT �폜�e�f \n"
						+ " FROM �r�l�P�P�o�א��^ \n"
						+ " WHERE ����b�c = '" + sData[0] + "' \n"
						+   " AND ����b�c = '" + sData[1] + "' \n"
						+   " AND ���^�m�n =  " + sData[2] + " \n"
						+   " FOR UPDATE \n"
						;
					reader = CmdSelect(sUser, conn2, cmdQuery);
					if(reader.Read()){
						s�폜�e�f = reader.GetString(0);
					}
					reader.Close();
					disposeReader(reader);
					reader = null;
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
					string s�i���L���S = (sData.Length > 32) ? sData[32] : " ";
					string s�i���L���T = (sData.Length > 33) ? sData[33] : " ";
					string s�i���L���U = (sData.Length > 34) ? sData[34] : " ";
					if(s�i���L���S.Length == 0) s�i���L���S = " ";
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
					if(s�폜�e�f == "0"){
						sRet[0] = "���ɂ��̓o�^�ԍ��͎g�p����Ă��܂��B\r\n"
								+ "�o�^�ԍ���ύX���Ă��������B";
						tran.Commit();
						logWriter(sUser, INF, sRet[0]);
						return sRet;
					}else if(s�폜�e�f == "1"){
					cmdQuery 
						= "UPDATE �r�l�P�P�o�א��^ \n"
						+    "SET ���^����   = '" + sData[4] + "', \n"
						+        "�t�@�C���� = '" + sData[5] + "', \n"
						+        "�׎�l�b�c = '" + sData[6] + "', \n"
						+        "�d�b�ԍ��P = '" + sData[7] + "', "
						+        "�d�b�ԍ��Q = '" + sData[8] + "', "
						+        "�d�b�ԍ��R = '" + sData[9] + "', \n"
						+        "�Z���P     = '" + sData[10] + "', "
						+        "�Z���Q     = '" + sData[11] + "', "
						+        "�Z���R     = '" + sData[12] + "', \n"
						+        "���O�P     = '" + sData[13] + "', "
						+        "���O�Q     = '" + sData[14] + "', \n"
						+        "�X�֔ԍ�   = '" + sData[15] + sData[16] + "', "
						+        "����v     = '" + s����v   + "', \n"
						+        "�ב��l�b�c = '" + sData[17] + "', "
						+        "�ב��l������ = '" + sData[18] + "', \n"
						+        "��       =  " + sData[19] + ", "
						+        "�ː�       =  " + sData[29] + ", "
						+        "�d��       =  " + sData[20] + ", \n"
						// ���j�b�g
						+        "�A���w���b�c�P = '" + sData[30] + "', "
						+        "�A���w���P = '" + sData[21] + "', \n"
						+        "�A���w���b�c�Q = '" + sData[31] + "', "
						+        "�A���w���Q = '" + sData[22] + "', \n"
						+        "�i���L���P = '" + sData[23] + "', "
						+        "�i���L���Q = '" + sData[24] + "', "
						+        "�i���L���R = '" + sData[25] + "', \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
						+        "�i���L���S = '" +s�i���L���S+ "', "
						+        "�i���L���T = '" +s�i���L���T+ "', "
						+        "�i���L���U = '" +s�i���L���U+ "', \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
						+        "�ی����z   =  " + sData[26] + ", \n"
						+        "�폜�e�f   = '0', \n"
						+        "�o�^����   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
						+        "�o�^�o�f   = '" + sData[27] + "',"
						+        "�o�^��     = '" + sData[28] + "',\n"
						+        "�X�V����   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
						+        "�X�V�o�f   = '" + sData[27] + "',"
						+        "�X�V��     = '" + sData[28] + "' \n"
						+ " WHERE ����b�c   = '" + sData[0] + "' \n"
						+    "AND ����b�c   = '" + sData[1] + "' \n"
						+    "AND ���^�m�n   =  " + sData[2] + " \n"
						;
					}else{
// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX END
					cmdQuery 
						= "INSERT INTO �r�l�P�P�o�א��^ \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
						+ "( \n"
						+ "����b�c, ����b�c, ���^�m�n \n"
						+ ", ���^����, �t�@�C����, �׎�l�b�c \n"
						+ ", �d�b�ԍ��P, �d�b�ԍ��Q, �d�b�ԍ��R \n"
						+ ", �Z���P, �Z���Q, �Z���R \n"
						+ ", ���O�P, ���O�Q, ���O�R \n"
						+ ", �X�֔ԍ� \n"
						+ ", ����v \n"
						+ ", �ב��l�b�c, �ב��l������ \n"
						+ ", ��, �ː�, �d��, ���j�b�g \n"
						+ ", �A���w���b�c�P, �A���w���P \n"
						+ ", �A���w���b�c�Q, �A���w���Q \n"
						+ ", �i���L���P, �i���L���Q, �i���L���R \n"
						+ ", �i���L���S, �i���L���T, �i���L���U \n"
						+ ", �����敪 \n"
						+ ", �ی����z \n"
						+ ", �폜�e�f, �o�^����, �o�^�o�f, �o�^�� \n"
						+ ", �X�V����, �X�V�o�f, �X�V�� \n"
						+ ") \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
						+ "VALUES ('" + sData[0] + "','" + sData[1] + "',"  + sData[2] + ", \n"
						+         "'" + sData[4] + "','" + sData[5] + "','" + sData[6] + "', \n"
						+         "'" + sData[7] + "','" + sData[8] + "','" + sData[9] + "', \n"
						+         "'" + sData[10] + "','" + sData[11] + "','" + sData[12] + "', \n"
						+         "'" + sData[13] + "','" + sData[14] + "',' ', \n"  // ���O�R
						+         "'" + sData[15] + sData[16] +"', \n"                // �X�֔ԍ�
						+         "'" + s����v + "', \n"
						+         "'" + sData[17] +"','" + sData[18] +"', \n"	// �ב��l�b�c  �ב��l������
						+         "" + sData[19] +"," + sData[29] +"," + sData[20] +",0, \n"
// MOD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� START
//						+         "'" + sData[21] +"','" + sData[22] +"','" + sData[23] +"', \n"
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
//						+         "'" + sData[30] +"','" + sData[21] +"','" + sData[31] +"','" + sData[22] +"','" + sData[23] +"', \n"
						+         "'" + sData[30] +"','" + sData[21] +"' \n" // �A���w���b�c�P, �A���w���P
						+         ",'" + sData[31] +"','" + sData[22] +"' \n" // �A���w���b�c�Q, �A���w���Q
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
// MOD 2005.05.30 ���s�j�ɉ� �A�����i�R�[�h�ǉ� END
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� START
//						+         "'" + sData[24] +"','" + sData[25] +"','1', \n" // �����敪[1]
						+         ",'" + sData[23] +"','" + sData[24] +"','" + sData[25] +"' \n" // �i���L���P�`�R
						+         ",'" + s�i���L���S +"','" + s�i���L���T +"','" + s�i���L���U +"' \n"
						+         ",'1', \n" // �����敪[1]
// MOD 2011.07.14 ���s�j���� �L���s�̒ǉ� END
						+         "" + sData[26] + ", \n"
						+         "'0', TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'" + sData[27] +"','" + sData[28] +"', \n"
						+         "TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'" + sData[27] +"','" + sData[28] +"') \n"
						;
// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX START
					}
// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX END

					CmdUpdate(sUser, conn2, cmdQuery);

					// ����}�X�^�̐��^�m�n�̍X�V
					// �����F����b�c�A����b�c�A���^�m�n�A�X�V�o�f�A�X�V��
					int iRet = Set_hinagataNo(sUser, conn2, new string[]{sData[0],sData[1],sData[2],sData[27],sData[28]});

					tran.Commit();

					sRet[0] = "����I��";
				}

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				tran.Rollback();
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				tran.Rollback();
				sRet[0] = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* ����}�X�^�̐��^�m�n�̎擾
		* �����F����b�c�A����b�c
		* �ߒl�F�X�e�[�^�X
		**********************************************************************/
		[WebMethod]
		public String[] Get_hinagataNo(string[] sUser, string sKey1, string sKey2)
		{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//			logFileOpen(sUser);
			logWriter(sUser, INF, "���^�m�n�擾�J�n");

			OracleConnection conn2 = null;
			string[] sRet = new string[2];

			// �c�a�ڑ�
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
				sRet[0] = "�c�a�ڑ��G���[";
				return sRet;
			}

// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� START
//			// ����`�F�b�N
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 ���s�j�����J ����`�F�b�N�ǉ� END

			string cmdQuery = "";
			try
			{
				cmdQuery
//					= "SELECT TO_CHAR(���^�m�n) \n"
					= "SELECT ���^�m�n \n"
					+ "  FROM �b�l�O�Q���� \n"
					+ " WHERE ����b�c = '" + sKey1 + "' \n"
					+   " AND ����b�c = '" + sKey2 + "' \n"
					+   " AND �폜�e�f = '0' \n"
					;

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				if (reader.Read())
				{
//					sRet[1] = reader.GetString(0);
					sRet[1] = reader.GetDecimal(0).ToString().Trim();
					sRet[0] = "����I��";
// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX START
					if(sRet[1] == "99"){
						for(int iCnt = 1; iCnt < 99; iCnt++){
							disposeReader(reader);
							reader = null;
							cmdQuery
								= "SELECT ���^�m�n \n"
								+ " FROM �r�l�P�P�o�א��^ \n"
								+ " WHERE ����b�c = '" + sKey1 + "' \n"
								+   " AND ����b�c = '" + sKey2 + "' \n"
								+   " AND ���^�m�n = " + iCnt + " \n"
								+   " AND �폜�e�f = '0' \n"
								;
							reader = CmdSelect(sUser, conn2, cmdQuery);
							if(reader.Read()){
								sRet[1] = iCnt.ToString().Trim();
							}else{
								break;
							}
						}
					}
// MOD 2010.09.07 ���s�j���� �f�t�H���g�̐��`�m�n�̎擾���@�̕ύX END
				}
				else
				{
					sRet[0] = "�Y���f�[�^������܂���";
				}
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "�T�[�o�G���[�F" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� START
				conn2 = null;
// ADD 2007.04.28 ���s�j���� �I�u�W�F�N�g�̔j�� END
// DEL 2007.05.10 ���s�j���� ���g�p�֐��̃R�����g��
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* ����}�X�^�̐��^�m�n�̍X�V
		* �����F����b�c�A����b�c�A���^�m�n�A�X�V�o�f�A�X�V��
		* �ߒl�F�X�e�[�^�X
		**********************************************************************/
		private int Set_hinagataNo(string[] sUser, OracleConnection conn2, string[] sKey)
		{
			int iRet      = 0;
			int i���^�m�n = int.Parse(sKey[2]);

			string cmdQuery 
				= "UPDATE �b�l�O�Q���� \n"
				+   " SET ���^�m�n = DECODE(SIGN(" + i���^�m�n + " - ���^�m�n), 1, " + i���^�m�n + ", ���^�m�n), \n"
				+       " �X�V���� = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
				+       " �X�V�o�f = '" + sKey[3] + "', \n"
				+       " �X�V��   = '" + sKey[4] + "' \n"
				+ " WHERE ����b�c = '" + sKey[0] + "' \n"
				+   " AND ����b�c = '" + sKey[1] + "' \n"
				+   " AND �폜�e�f = '0' \n"
				;

			iRet = CmdUpdate(sUser, conn2, cmdQuery);

			return iRet;
		}

	}
}
