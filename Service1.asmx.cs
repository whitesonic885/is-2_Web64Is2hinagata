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
	// 修正履歴
	//--------------------------------------------------------------------------
	// ADD 2007.04.28 東都）高木 オブジェクトの破棄
	//	disposeReader(reader);
	//	reader = null;
	// DEL 2007.05.10 東都）高木 未使用関数のコメント化
	//	logFileOpen(sUser);
	//	userCheck2(conn2, sUser);
	//	logFileClose();
	//--------------------------------------------------------------------------
	// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 
	//--------------------------------------------------------------------------

	[System.Web.Services.WebService(
		 Namespace="http://Walkthrough/XmlWebServices/",
		 Description="is2hinagata")]

	public class Service1 : is2common.CommService
	{
		public Service1()
		{
			//CODEGEN: この呼び出しは、ASP.NET Web サービス デザイナで必要です。
			InitializeComponent();

			connectService();
		}

		#region コンポーネント デザイナで生成されたコード 
		
		//Web サービス デザイナで必要です。
		private IContainer components = null;
				
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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
		* 雛型一覧の取得
		* 引数：会員ＣＤ、部門ＣＤ
		* 戻値：ステータス、件数、雛型ＮＯ、雛型名称、ファイル名
		**********************************************************************/
		[WebMethod]
		public String[] Get_hinagata(string[] sUser, String sKey1, String sKey2)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型一覧取得開始");

// MOD 2005.05.11 東都）高木 ORA-03113対策？ START
//			string[] sRet = new string[1];
			OracleConnection conn2 = null;
			ArrayList sList = new ArrayList();
			string[] sRet = new string[2];
// MOD 2005.05.11 東都）高木 ORA-03113対策？ END

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet[0] = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			string cmdQuery = "";
			string sCnt = "0";
			int    iCnt = 0;
			try
			{
// MOD 2005.05.11 東都）高木 ORA-03113対策？ START
//				cmdQuery
////					= "SELECT TO_CHAR(COUNT(*)) \n"
////					= "SELECT NVL(COUNT(*),0) \n"
//					= "SELECT COUNT(*) \n"
//					+   "FROM ＳＭ１１出荷雛型 \n"
//					+  "WHERE 会員ＣＤ = '" + sKey1 + "' \n"
//					+    "AND 部門ＣＤ = '" + sKey2 + "' \n"
//					+    "AND 削除ＦＧ = '0' \n";
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
//						= "SELECT 雛型名称, TO_CHAR(雛型ＮＯ), ファイル名, \n"
//						+        "TO_CHAR(更新日時) \n"
//						+   "FROM ＳＭ１１出荷雛型 \n"
//						+  "WHERE 会員ＣＤ = '" + sKey1 + "' \n"
//						+    "AND 部門ＣＤ = '" + sKey2 + "' \n"
//						+    "AND 削除ＦＧ = '0' \n"
//						+  "ORDER BY 雛型ＮＯ \n";
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
//				sRet[0] = "正常終了";

				cmdQuery
					= "SELECT 雛型名称, 雛型ＮＯ, ファイル名, 更新日時 \n"
					+ " FROM ＳＭ１１出荷雛型 \n"
					+ " WHERE 会員ＣＤ = '" + sKey1 + "' \n"
					+ " AND 部門ＣＤ = '" + sKey2 + "' \n"
					+ " AND 削除ＦＧ = '0' \n"
					+ " ORDER BY 雛型ＮＯ \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				while (reader.Read())
				{
					sList.Add(reader.GetString(0).Trim());
					sList.Add(reader.GetDecimal(1).ToString().Trim());
					sList.Add(reader.GetString(2).Trim());
					sList.Add(reader.GetDecimal(3).ToString().Trim());
					iCnt++;
				}
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END

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
					sRet[0] = "正常終了";
					sRet[1] = sCnt;
				}
// MOD 2005.05.11 東都）高木 ORA-03113対策？ END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* 雛型の削除
		* 引数：会員ＣＤ、部門ＣＤ、雛型ＮＯ、更新日時、更新ＰＧ、更新者
		* 戻値：ステータス
		**********************************************************************/
		[WebMethod]
		public String Del_hinagata(string[] sUser, String[] sKey)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型削除開始");

			OracleConnection conn2 = null;
			string sRet = "";

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet = userCheck2(conn2, sUser);
//			if(sRet.Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			string sQuery = "";
			OracleTransaction tran;
			tran = conn2.BeginTransaction();
			try
			{
				sQuery
					= "UPDATE ＳＭ１１出荷雛型 \n"
					+    "SET 削除ＦＧ = '1', \n"
					+        "更新日時 = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
					+        "更新ＰＧ = '" + sKey[4] + "', \n"
					+        "更新者   = '" + sKey[5] + "' \n"
					+  "WHERE 会員ＣＤ = '" + sKey[0] + "' \n"
					+    "AND 部門ＣＤ = '" + sKey[1] + "' \n"
					+    "AND 雛型ＮＯ =  " + sKey[2] + " \n"
					+    "AND 削除ＦＧ = '0' \n"
					+    "AND 更新日時 =  " + sKey[3] + " \n";

				int iUpdRow = CmdUpdate(sUser, conn2, sQuery);

				tran.Commit();
				if(iUpdRow == 0)
					sRet = "排他エラー：他の端末で既に修正されていました";
				else				
					sRet = "正常終了";

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
				sRet = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* 雛型データの取得
		* 引数：会員ＣＤ、部門ＣＤ、雛型ＮＯ
		* 戻値：ステータス、雛型の項目
		**********************************************************************/
		[WebMethod]
		public String[] Get_Hinagata2(string[] sUser, String sKey1, String sKey2, int iKey3)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型データ取得開始");

// MOD 2005.06.01 東都）伊賀 指定日区分追加 START
// MOD 2005.05.13 東都）小童谷 荷送人重量追加 START
//			string[] sRet = new string[36];
// MOD 2005.05.17 東都）小童谷 才数追加 START
//			string[] sRet = new string[37];
//			string[] sRet = new string[39];
			OracleConnection conn2 = null;
// MOD 2011.07.14 東都）高木 記事行の追加 START
//			string[] sRet = new string[41];
			string[] sRet = new string[44];
// MOD 2011.07.14 東都）高木 記事行の追加 END
// MOD 2005.05.17 東都）小童谷 才数追加 END
// MOD 2005.05.13 東都）小童谷 荷送人重量追加 END
// MOD 2005.06.01 東都）伊賀 指定日区分追加 END

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet[0] = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			string cmdQuery = "";
			try
			{
				cmdQuery
					= "SELECT "
					+ " S.雛型名称  , S.ファイル名, S.荷受人ＣＤ   , S.電話番号１, \n"
					+ " S.電話番号２, S.電話番号３, S.住所１       , S.住所２    , \n"
					+ " S.住所３    , S.名前１    , S.名前２       , SUBSTR(S.郵便番号,1,3), \n"
					+ " SUBSTR(S.郵便番号,4,4), S.荷送人ＣＤ, S.荷送人部署名 , TO_CHAR(S.個数), \n"
					+ " TO_CHAR(S.重量), S.輸送指示１, S.輸送指示２, S.品名記事１, \n"
					+ " S.品名記事２, S.品名記事３, TO_CHAR(S.保険金額), TO_CHAR(S.更新日時), \n"
					+ " NVL(N.電話番号１,' '), NVL(N.電話番号２,' '), NVL(N.電話番号３,' '), \n"
					+ " NVL(N.住所１,' ')    , NVL(N.住所２,' ')    , NVL(N.名前１,' '), \n"
					+ " NVL(N.名前２,' ')    , SUBSTR(NVL(N.郵便番号,'   '),1,3), \n"
					+ " SUBSTR(NVL(N.郵便番号,'       '),4,4), \n"
// MOD 2005.05.13 東都）小童谷 荷送人重量追加 START
//					+ " NVL(N.得意先ＣＤ,' '), NVL(N.得意先部課ＣＤ,' ') \n"
					+ " NVL(N.得意先ＣＤ,' '), NVL(N.得意先部課ＣＤ,' '),TO_CHAR(NVL(N.重量,'0')), \n"
// MOD 2005.05.13 東都）小童谷 荷送人重量追加 END
// ADD 2005.05.17 東都）小童谷 才数追加 START
					+ " TO_CHAR(NVL(S.才数,'0')),TO_CHAR(NVL(N.才数,'0')) \n"
// ADD 2005.05.17 東都）小童谷 才数追加 END
// ADD 2005.06.01 東都）伊賀 輸送商品コード追加 START
					+ ",S.輸送指示ＣＤ１,S.輸送指示ＣＤ２ \n"
// ADD 2005.06.01 東都）伊賀 輸送商品コード追加 END
// MOD 2011.07.14 東都）高木 記事行の追加 START
					+ ", S.品名記事４, S.品名記事５, S.品名記事６ \n"
// MOD 2011.07.14 東都）高木 記事行の追加 END
					+ "  FROM ＳＭ１１出荷雛型 S, \n"
					+ "       ＳＭ０１荷送人   N \n"
					+ " WHERE S.会員ＣＤ = '" + sKey1 + "' \n"
					+   " AND S.部門ＣＤ = '" + sKey2 + "' \n"
					+   " AND S.雛型ＮＯ =  " + iKey3
					+   " AND S.削除ＦＧ = '0' \n"
					+   " AND '"+ sKey1 +"' = N.会員ＣＤ(+) \n"
					+   " AND '"+ sKey2 +"' = N.部門ＣＤ(+) \n"
					+   " AND S.荷送人ＣＤ  = N.荷送人ＣＤ(+) \n"
					+   " AND '0'           = N.削除ＦＧ(+) \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				if (reader.Read())
				{
					for(int iCnt = 1; iCnt < sRet.Length; iCnt++)
					{
						sRet[iCnt] = reader.GetString(iCnt - 1).TrimEnd();
					}
					sRet[0] = "正常終了";
				}
				else
				{
					sRet[0] = "該当データがありません";
				}
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}

			return sRet;
		}

		/*********************************************************************
		* 雛型データの更新
		* 引数：...
		* 戻値：ステータス
		**********************************************************************/
		[WebMethod]
		public String[] Upd_hinagata(string[] sUser, string[] sData)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型データ更新開始");

			OracleConnection conn2 = null;
			string[] sRet = new string[5];

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet[0] = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			OracleTransaction tran;
			tran = conn2.BeginTransaction();

// 荷送人ＣＤ存在チェックの変更 Start
//			string s得意先ＣＤ     = " ";
//			string s得意先部課ＣＤ = " ";
//			string s得意先部課名   = " ";
//			string s特殊計         = " ";
//			try
//			{
//				//荷送人ＣＤ存在チェック（得意先情報の取得）
//				string cmdQuery
//					= "SELECT N.得意先ＣＤ, N.得意先部課ＣＤ, S.得意先部課名 "
//					+  "FROM ＣＭ０２部門 B, "
//					+       "ＳＭ０１荷送人 N, "
//					+       "ＳＭ０４請求先 S "
//					+ "WHERE B.会員ＣＤ   = '" + sData[0]  +"' "
//					+   "AND B.部門ＣＤ   = '" + sData[1]  +"' "
//					+   "AND B.削除ＦＧ   = '0' "
//					+   "AND N.会員ＣＤ   = '" + sData[0]  +"' "
//					+   "AND N.部門ＣＤ   = '" + sData[1]  +"' "
//					+   "AND N.荷送人ＣＤ = '" + sData[17] +"' "
//					+   "AND N.削除ＦＧ   = '0' "
//					+   "AND B.郵便番号       = S.郵便番号 "
//					+   "AND N.得意先ＣＤ     = S.得意先ＣＤ "
//					+   "AND N.得意先部課ＣＤ = S.得意先部課ＣＤ "
//					;
//
//				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);
//
//				if( !reader.Read() ){
//					sRet[0] = "0";
//				}
//				else
//				{
//					s得意先ＣＤ     = reader.GetString(0);
//					s得意先部課ＣＤ = reader.GetString(1);
//					s得意先部課名   = reader.GetString(2);

			decimal d件数;
			string s特殊計         = " ";
			try
			{
				//荷送人ＣＤ存在チェック（得意先情報の取得）
				string cmdQuery
//					= "SELECT NVL(COUNT(*),0) FROM ＳＭ０１荷送人 \n"
//					= "SELECT COUNT(*) FROM ＳＭ０１荷送人 \n"
					= "SELECT COUNT(ROWID) FROM ＳＭ０１荷送人 \n"
					+  "WHERE 会員ＣＤ   = '" + sData[0]  + "' \n"
					+    "AND 部門ＣＤ   = '" + sData[1]  + "' \n"
					+    "AND 荷送人ＣＤ = '" + sData[17] + "' \n"
					+    "AND 削除ＦＧ   = '0' \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				reader.Read();
				d件数   = reader.GetDecimal(0);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
				if(d件数 == 0)
				{
					sRet[0] = "0";
				}
				else
				{
// 荷送人ＣＤ存在チェックの変更 End

					//特殊計取得
					if( sData[6] != " " )
					{
						cmdQuery
							= "SELECT NVL(特殊計,' ') \n"
							+  "FROM ＳＭ０２荷受人 \n"
							+ "WHERE 会員ＣＤ   = '" + sData[0] +"' \n"
							+   "AND 部門ＣＤ   = '" + sData[1] +"' \n"
							+   "AND 荷受人ＣＤ = '" + sData[6] +"' \n"
							+   "AND 削除ＦＧ   = '0' \n";

						reader = CmdSelect(sUser, conn2, cmdQuery);

						bool bRead = reader.Read();
						if(bRead == true)
							s特殊計   = reader.GetString(0);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
						disposeReader(reader);
						reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
					}

					int iPos = 4;
// MOD 2011.07.14 東都）高木 記事行の追加 START
					string s品名記事４ = (sData.Length > 32) ? sData[32] : " ";
					string s品名記事５ = (sData.Length > 33) ? sData[33] : " ";
					string s品名記事６ = (sData.Length > 34) ? sData[34] : " ";
					if(s品名記事４.Length == 0) s品名記事４ = " ";
// MOD 2011.07.14 東都）高木 記事行の追加 END
					cmdQuery 
						= "UPDATE ＳＭ１１出荷雛型 \n"
						+    "SET 雛型名称   = '" + sData[iPos++] + "', \n"
						+        "ファイル名 = '" + sData[iPos++] + "', "
						+        "荷受人ＣＤ = '" + sData[iPos++] + "', "
						+        "電話番号１ = '" + sData[iPos++] + "', "
						+        "電話番号２ = '" + sData[iPos++] + "', \n"
						+        "電話番号３ = '" + sData[iPos++] + "', "
						+        "住所１     = '" + sData[iPos++] + "', "
						+        "住所２     = '" + sData[iPos++] + "', "
						+        "住所３     = '" + sData[iPos++] + "', \n"
						+        "名前１     = '" + sData[iPos++] + "', "
						+        "名前２     = '" + sData[iPos++] + "', "
						+        "郵便番号   = '" + sData[iPos++] + sData[iPos++] + "', "
						+        "特殊計     = '" + s特殊計   + "', \n"
						+        "荷送人ＣＤ = '" + sData[iPos++] + "', "
						+        "荷送人部署名 = '" + sData[iPos++] + "', "
						+        "個数       =  " + sData[iPos++] + ", "
// ADD 2005.05.17 東都）小童谷 才数追加 START
						+        "才数       =  " + sData[29] + ", \n"
// ADD 2005.05.17 東都）小童谷 才数追加 END
						+        "重量       =  " + sData[iPos++] + ", \n"
// ADD 2005.05.30 東都）伊賀 輸送商品コード追加 START
						+        "輸送指示ＣＤ１ = '" + sData[30] + "', "
// ADD 2005.05.30 東都）伊賀 輸送商品コード追加 END
						+        "輸送指示１ = '" + sData[iPos++] + "', "
// ADD 2005.05.30 東都）伊賀 輸送商品コード追加 START
						+        "輸送指示ＣＤ２ = '" + sData[31] + "', "
// ADD 2005.05.30 東都）伊賀 輸送商品コード追加 END
						+        "輸送指示２ = '" + sData[iPos++] + "', "
						+        "品名記事１ = '" + sData[iPos++] + "', "
						+        "品名記事２ = '" + sData[iPos++] + "', \n"
						+        "品名記事３ = '" + sData[iPos++] + "', "
// MOD 2011.07.14 東都）高木 記事行の追加 START
						+        "品名記事４ = '" +s品名記事４+ "', "
						+        "品名記事５ = '" +s品名記事５+ "', "
						+        "品名記事６ = '" +s品名記事６+ "', \n"
// MOD 2011.07.14 東都）高木 記事行の追加 END
						+        "保険金額   =  " + sData[iPos++] + ", "
						+        "更新ＰＧ   = '" + sData[iPos++] + "',"
						+        "更新者     = '" + sData[iPos++] + "',\n"
						+        "更新日時   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') \n"
						+ " WHERE 会員ＣＤ   = '" + sData[0] + "' \n"
						+    "AND 部門ＣＤ   = '" + sData[1] + "' \n"
						+    "AND 雛型ＮＯ   =  " + sData[2] + " \n"
						+    "AND 更新日時   =  " + sData[3] + " \n";

					int i更新件数 = CmdUpdate(sUser, conn2, cmdQuery);

					// 部門マスタの雛型ＮＯの更新
					// 引数：会員ＣＤ、部門ＣＤ、雛型ＮＯ、更新ＰＧ、更新者
					int iRet = Set_hinagataNo(sUser, conn2, new string[]{sData[0],sData[1],sData[2],sData[27],sData[28]});

					tran.Commit();
					if(i更新件数 == 0)
						sRet[0] = "排他エラー：他の端末で既に修正されていました";
					else				
						sRet[0] = "正常終了";
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
				sRet[0] = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* 雛型データの追加
		* 引数：...
		* 戻値：ステータス
		**********************************************************************/
		[WebMethod]
		public String[] Ins_hinagata(string[] sUser, string[] sData)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型データ追加開始");

			OracleConnection conn2 = null;
			string[] sRet = new string[5];

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet[0] = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			OracleTransaction tran;
			tran = conn2.BeginTransaction();

			decimal d件数;
			string s特殊計 = " ";
			string cmdQuery = "";
			try
			{
				//荷送人ＣＤ存在チェック
				cmdQuery
//					= "SELECT NVL(COUNT(*),0) FROM ＳＭ０１荷送人 \n"
//					= "SELECT COUNT(*) FROM ＳＭ０１荷送人 \n"
					= "SELECT COUNT(ROWID) FROM ＳＭ０１荷送人 \n"
					+  "WHERE 会員ＣＤ   = '" + sData[0]  + "' \n"
					+    "AND 部門ＣＤ   = '" + sData[1]  + "' \n"
					+    "AND 荷送人ＣＤ = '" + sData[17] + "' \n"
					+    "AND 削除ＦＧ   = '0' \n";

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				reader.Read();
				d件数   = reader.GetDecimal(0);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
				if(d件数 == 0)
				{
					sRet[0] = "0";
				}
				else
				{
					//特殊計取得
					if(sData[6] != " ")
					{
						cmdQuery
							= "SELECT NVL(特殊計,' ') FROM ＳＭ０２荷受人 \n"
							+  "WHERE 会員ＣＤ   = '" + sData[0] + "' \n"
							+    "AND 部門ＣＤ   = '" + sData[1] + "' \n"
							+    "AND 荷受人ＣＤ = '" + sData[6] + "' \n"
							+    "AND 削除ＦＧ   = '0' \n";

						reader = CmdSelect(sUser, conn2, cmdQuery);

						bool bRead = reader.Read();
						if(bRead == true)
							s特殊計   = reader.GetString(0);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
						disposeReader(reader);
						reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
					}

// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 START
//					// 同じキーで削除フラグがついていた場合には削除
//					cmdQuery 
//						= "DELETE FROM ＳＭ１１出荷雛型 \n"
//						+ " WHERE 会員ＣＤ = '" + sData[0] + "' \n"
//						+   " AND 部門ＣＤ = '" + sData[1] + "' \n"
//						+   " AND 雛型ＮＯ =  " + sData[2] + " \n"
//						+   " AND 削除ＦＧ = '1' \n";
//
//					CmdUpdate(sUser, conn2, cmdQuery);
					string s削除ＦＧ = "";
					cmdQuery 
						= "SELECT 削除ＦＧ \n"
						+ " FROM ＳＭ１１出荷雛型 \n"
						+ " WHERE 会員ＣＤ = '" + sData[0] + "' \n"
						+   " AND 部門ＣＤ = '" + sData[1] + "' \n"
						+   " AND 雛型ＮＯ =  " + sData[2] + " \n"
						+   " FOR UPDATE \n"
						;
					reader = CmdSelect(sUser, conn2, cmdQuery);
					if(reader.Read()){
						s削除ＦＧ = reader.GetString(0);
					}
					reader.Close();
					disposeReader(reader);
					reader = null;
// MOD 2011.07.14 東都）高木 記事行の追加 START
					string s品名記事４ = (sData.Length > 32) ? sData[32] : " ";
					string s品名記事５ = (sData.Length > 33) ? sData[33] : " ";
					string s品名記事６ = (sData.Length > 34) ? sData[34] : " ";
					if(s品名記事４.Length == 0) s品名記事４ = " ";
// MOD 2011.07.14 東都）高木 記事行の追加 END
					if(s削除ＦＧ == "0"){
						sRet[0] = "既にその登録番号は使用されています。\r\n"
								+ "登録番号を変更してください。";
						tran.Commit();
						logWriter(sUser, INF, sRet[0]);
						return sRet;
					}else if(s削除ＦＧ == "1"){
					cmdQuery 
						= "UPDATE ＳＭ１１出荷雛型 \n"
						+    "SET 雛型名称   = '" + sData[4] + "', \n"
						+        "ファイル名 = '" + sData[5] + "', \n"
						+        "荷受人ＣＤ = '" + sData[6] + "', \n"
						+        "電話番号１ = '" + sData[7] + "', "
						+        "電話番号２ = '" + sData[8] + "', "
						+        "電話番号３ = '" + sData[9] + "', \n"
						+        "住所１     = '" + sData[10] + "', "
						+        "住所２     = '" + sData[11] + "', "
						+        "住所３     = '" + sData[12] + "', \n"
						+        "名前１     = '" + sData[13] + "', "
						+        "名前２     = '" + sData[14] + "', \n"
						+        "郵便番号   = '" + sData[15] + sData[16] + "', "
						+        "特殊計     = '" + s特殊計   + "', \n"
						+        "荷送人ＣＤ = '" + sData[17] + "', "
						+        "荷送人部署名 = '" + sData[18] + "', \n"
						+        "個数       =  " + sData[19] + ", "
						+        "才数       =  " + sData[29] + ", "
						+        "重量       =  " + sData[20] + ", \n"
						// ユニット
						+        "輸送指示ＣＤ１ = '" + sData[30] + "', "
						+        "輸送指示１ = '" + sData[21] + "', \n"
						+        "輸送指示ＣＤ２ = '" + sData[31] + "', "
						+        "輸送指示２ = '" + sData[22] + "', \n"
						+        "品名記事１ = '" + sData[23] + "', "
						+        "品名記事２ = '" + sData[24] + "', "
						+        "品名記事３ = '" + sData[25] + "', \n"
// MOD 2011.07.14 東都）高木 記事行の追加 START
						+        "品名記事４ = '" +s品名記事４+ "', "
						+        "品名記事５ = '" +s品名記事５+ "', "
						+        "品名記事６ = '" +s品名記事６+ "', \n"
// MOD 2011.07.14 東都）高木 記事行の追加 END
						+        "保険金額   =  " + sData[26] + ", \n"
						+        "削除ＦＧ   = '0', \n"
						+        "登録日時   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
						+        "登録ＰＧ   = '" + sData[27] + "',"
						+        "登録者     = '" + sData[28] + "',\n"
						+        "更新日時   = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
						+        "更新ＰＧ   = '" + sData[27] + "',"
						+        "更新者     = '" + sData[28] + "' \n"
						+ " WHERE 会員ＣＤ   = '" + sData[0] + "' \n"
						+    "AND 部門ＣＤ   = '" + sData[1] + "' \n"
						+    "AND 雛型ＮＯ   =  " + sData[2] + " \n"
						;
					}else{
// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 END
					cmdQuery 
						= "INSERT INTO ＳＭ１１出荷雛型 \n"
// MOD 2011.07.14 東都）高木 記事行の追加 START
						+ "( \n"
						+ "会員ＣＤ, 部門ＣＤ, 雛型ＮＯ \n"
						+ ", 雛型名称, ファイル名, 荷受人ＣＤ \n"
						+ ", 電話番号１, 電話番号２, 電話番号３ \n"
						+ ", 住所１, 住所２, 住所３ \n"
						+ ", 名前１, 名前２, 名前３ \n"
						+ ", 郵便番号 \n"
						+ ", 特殊計 \n"
						+ ", 荷送人ＣＤ, 荷送人部署名 \n"
						+ ", 個数, 才数, 重量, ユニット \n"
						+ ", 輸送指示ＣＤ１, 輸送指示１ \n"
						+ ", 輸送指示ＣＤ２, 輸送指示２ \n"
						+ ", 品名記事１, 品名記事２, 品名記事３ \n"
						+ ", 品名記事４, 品名記事５, 品名記事６ \n"
						+ ", 元着区分 \n"
						+ ", 保険金額 \n"
						+ ", 削除ＦＧ, 登録日時, 登録ＰＧ, 登録者 \n"
						+ ", 更新日時, 更新ＰＧ, 更新者 \n"
						+ ") \n"
// MOD 2011.07.14 東都）高木 記事行の追加 END
						+ "VALUES ('" + sData[0] + "','" + sData[1] + "',"  + sData[2] + ", \n"
						+         "'" + sData[4] + "','" + sData[5] + "','" + sData[6] + "', \n"
						+         "'" + sData[7] + "','" + sData[8] + "','" + sData[9] + "', \n"
						+         "'" + sData[10] + "','" + sData[11] + "','" + sData[12] + "', \n"
						+         "'" + sData[13] + "','" + sData[14] + "',' ', \n"  // 名前３
						+         "'" + sData[15] + sData[16] +"', \n"                // 郵便番号
						+         "'" + s特殊計 + "', \n"
						+         "'" + sData[17] +"','" + sData[18] +"', \n"	// 荷送人ＣＤ  荷送人部署名
						+         "" + sData[19] +"," + sData[29] +"," + sData[20] +",0, \n"
// MOD 2005.05.30 東都）伊賀 輸送商品コード追加 START
//						+         "'" + sData[21] +"','" + sData[22] +"','" + sData[23] +"', \n"
// MOD 2011.07.14 東都）高木 記事行の追加 START
//						+         "'" + sData[30] +"','" + sData[21] +"','" + sData[31] +"','" + sData[22] +"','" + sData[23] +"', \n"
						+         "'" + sData[30] +"','" + sData[21] +"' \n" // 輸送指示ＣＤ１, 輸送指示１
						+         ",'" + sData[31] +"','" + sData[22] +"' \n" // 輸送指示ＣＤ２, 輸送指示２
// MOD 2011.07.14 東都）高木 記事行の追加 END
// MOD 2005.05.30 東都）伊賀 輸送商品コード追加 END
// MOD 2011.07.14 東都）高木 記事行の追加 START
//						+         "'" + sData[24] +"','" + sData[25] +"','1', \n" // 元着区分[1]
						+         ",'" + sData[23] +"','" + sData[24] +"','" + sData[25] +"' \n" // 品名記事１～３
						+         ",'" + s品名記事４ +"','" + s品名記事５ +"','" + s品名記事６ +"' \n"
						+         ",'1', \n" // 元着区分[1]
// MOD 2011.07.14 東都）高木 記事行の追加 END
						+         "" + sData[26] + ", \n"
						+         "'0', TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'" + sData[27] +"','" + sData[28] +"', \n"
						+         "TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'" + sData[27] +"','" + sData[28] +"') \n"
						;
// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 START
					}
// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 END

					CmdUpdate(sUser, conn2, cmdQuery);

					// 部門マスタの雛型ＮＯの更新
					// 引数：会員ＣＤ、部門ＣＤ、雛型ＮＯ、更新ＰＧ、更新者
					int iRet = Set_hinagataNo(sUser, conn2, new string[]{sData[0],sData[1],sData[2],sData[27],sData[28]});

					tran.Commit();

					sRet[0] = "正常終了";
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
				sRet[0] = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* 部門マスタの雛型ＮＯの取得
		* 引数：会員ＣＤ、部門ＣＤ
		* 戻値：ステータス
		**********************************************************************/
		[WebMethod]
		public String[] Get_hinagataNo(string[] sUser, string sKey1, string sKey2)
		{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//			logFileOpen(sUser);
			logWriter(sUser, INF, "雛型ＮＯ取得開始");

			OracleConnection conn2 = null;
			string[] sRet = new string[2];

			// ＤＢ接続
			conn2 = connect2(sUser);
			if(conn2 == null)
			{
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
				sRet[0] = "ＤＢ接続エラー";
				return sRet;
			}

// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 START
//			// 会員チェック
//			sRet[0] = userCheck2(conn2, sUser);
//			if(sRet[0].Length > 0)
//			{
//				disconnect2(sUser, conn2);
//				logFileClose();
//				return sRet;
//			}
//// ADD 2005.05.23 東都）小童谷 会員チェック追加 END

			string cmdQuery = "";
			try
			{
				cmdQuery
//					= "SELECT TO_CHAR(雛型ＮＯ) \n"
					= "SELECT 雛型ＮＯ \n"
					+ "  FROM ＣＭ０２部門 \n"
					+ " WHERE 会員ＣＤ = '" + sKey1 + "' \n"
					+   " AND 部門ＣＤ = '" + sKey2 + "' \n"
					+   " AND 削除ＦＧ = '0' \n"
					;

				OracleDataReader reader = CmdSelect(sUser, conn2, cmdQuery);

				if (reader.Read())
				{
//					sRet[1] = reader.GetString(0);
					sRet[1] = reader.GetDecimal(0).ToString().Trim();
					sRet[0] = "正常終了";
// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 START
					if(sRet[1] == "99"){
						for(int iCnt = 1; iCnt < 99; iCnt++){
							disposeReader(reader);
							reader = null;
							cmdQuery
								= "SELECT 雛型ＮＯ \n"
								+ " FROM ＳＭ１１出荷雛型 \n"
								+ " WHERE 会員ＣＤ = '" + sKey1 + "' \n"
								+   " AND 部門ＣＤ = '" + sKey2 + "' \n"
								+   " AND 雛型ＮＯ = " + iCnt + " \n"
								+   " AND 削除ＦＧ = '0' \n"
								;
							reader = CmdSelect(sUser, conn2, cmdQuery);
							if(reader.Read()){
								sRet[1] = iCnt.ToString().Trim();
							}else{
								break;
							}
						}
					}
// MOD 2010.09.07 東都）高木 デフォルトの雛形ＮＯの取得方法の変更 END
				}
				else
				{
					sRet[0] = "該当データがありません";
				}
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				disposeReader(reader);
				reader = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END

				logWriter(sUser, INF, sRet[0]);
			}
			catch (OracleException ex)
			{
				sRet[0] = chgDBErrMsg(sUser, ex);
			}
			catch (Exception ex)
			{
				sRet[0] = "サーバエラー：" + ex.Message;
				logWriter(sUser, ERR, sRet[0]);
			}
			finally
			{
				disconnect2(sUser, conn2);
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 START
				conn2 = null;
// ADD 2007.04.28 東都）高木 オブジェクトの破棄 END
// DEL 2007.05.10 東都）高木 未使用関数のコメント化
//				logFileClose();
			}
			
			return sRet;
		}

		/*********************************************************************
		* 部門マスタの雛型ＮＯの更新
		* 引数：会員ＣＤ、部門ＣＤ、雛型ＮＯ、更新ＰＧ、更新者
		* 戻値：ステータス
		**********************************************************************/
		private int Set_hinagataNo(string[] sUser, OracleConnection conn2, string[] sKey)
		{
			int iRet      = 0;
			int i雛型ＮＯ = int.Parse(sKey[2]);

			string cmdQuery 
				= "UPDATE ＣＭ０２部門 \n"
				+   " SET 雛型ＮＯ = DECODE(SIGN(" + i雛型ＮＯ + " - 雛型ＮＯ), 1, " + i雛型ＮＯ + ", 雛型ＮＯ), \n"
				+       " 更新日時 = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'), \n"
				+       " 更新ＰＧ = '" + sKey[3] + "', \n"
				+       " 更新者   = '" + sKey[4] + "' \n"
				+ " WHERE 会員ＣＤ = '" + sKey[0] + "' \n"
				+   " AND 部門ＣＤ = '" + sKey[1] + "' \n"
				+   " AND 削除ＦＧ = '0' \n"
				;

			iRet = CmdUpdate(sUser, conn2, cmdQuery);

			return iRet;
		}

	}
}
