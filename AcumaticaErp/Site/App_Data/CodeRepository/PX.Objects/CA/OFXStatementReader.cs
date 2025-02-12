using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using PX.Data;
using System.Globalization;
using PX.Api;
using PX.SM;
#if USE_SGML_PARSER
using Sgml;
#endif
namespace PX.Objects.CA
{
	

	public class OFXStatementReader : StatementReader, IStatementReader 
    {
        #region Private members
        private OFXHeader header = new OFXHeader();
        private STMTTRNRS content = new STMTTRNRS();
        private bool useConverter = true;


        #endregion
        #region Internal Type Declaration
        #region OFX Data Classes
        public class OFXMessage
        {
            public STMTTRNRS Stmttrnrs;
        }
        public class STMTTRNRS
        {
            private string _TRNUID;
            private List<STMTRS> _Stmtrs;
            public string TRNUID
            {
                get { return _TRNUID; }
                set { _TRNUID = value; }
            }
            public List<STMTRS> Stmtrs
            {
                get
                {
                    if (this._Stmtrs == null)
                        this._Stmtrs = new List<STMTRS>(1);
                    return this._Stmtrs;
                }
            }
            public void Clear()
            {
                this.TRNUID = string.Empty;
                if (this._Stmtrs != null)
                {
                    this._Stmtrs.Clear();
                }
            }
        }
        public class STMTRS
        {
            #region Private Members;
            private string _CURDEF;
            private BalanceInfo _LEDGERBAL;
            private BalanceInfo _AVAILBAL;
            private BankAcctInfo _BANKACCTFROM;
            private CCAcctInfo _CCACCTFROM;
            private BankTransList _BANKTRANLIST;
            public STMTRS() 
            {
                this._CCACCTFROM = null;
                this._BANKACCTFROM = null;
            }
            #endregion
            #region Public Members
            public string CURDEF
            {
                get { return _CURDEF; }
                set { _CURDEF = value; }
            }
            public BankAcctInfo BANKACCTFROM
            {
                get
                {
                    if (this._BANKACCTFROM == null)
                        this._BANKACCTFROM = new BankAcctInfo();
                    return this._BANKACCTFROM;
                }
            }

            public CCAcctInfo CCACCTFROM
            {
                get
                {
                    if (this._CCACCTFROM == null)
                        this._CCACCTFROM = new CCAcctInfo();
                    return this._CCACCTFROM;
                }
            }
            public BankTransList BANKTRANLIST
            {
                get
                {
                    if (this._BANKTRANLIST == null)
                        this._BANKTRANLIST = new BankTransList();
                    return this._BANKTRANLIST;
                }
            }

            public BalanceInfo LEDGERBAL
            {
                get
                {
                    if (this._LEDGERBAL == null)
                        this._LEDGERBAL = new BalanceInfo();
                    return this._LEDGERBAL;
                }
            }
            public BalanceInfo AVAILBAL
            {
                get
                {
                    if (this._AVAILBAL == null)
                        this._AVAILBAL = new BalanceInfo();
                    return this._AVAILBAL;
                }
            }

            public bool IsBankAccount() 
            {
                return (this._BANKACCTFROM != null && this._CCACCTFROM == null);
            }
            public bool IsCCAccount()
            {
                return (this._BANKACCTFROM == null && this._CCACCTFROM != null);
            }
            public bool HasAccountInfo() 
            {
                return (this.IsBankAccount() || this.IsCCAccount());
            }
            #endregion

        }
        public class BankTransList
        {
            private DateTime? _DTSTART;
            private DateTime? _DTEND;
            public DateTime? DTSTART
            {
                get { return _DTSTART; }
                set { _DTSTART = value; }
            }
            public DateTime? DTEND
            {
                get { return _DTEND; }
                set { _DTEND = value; }
            }

            private List<STMTTRN> _trans;

            public List<STMTTRN> Trans
            {
                get
                {
                    if (this._trans == null)
                        this._trans = new List<STMTTRN>();
                    return this._trans;
                }
            }


        }
        public class BankAcctInfo
        {
            private string _BANKACCTFROM;
            private string _BANKID;
            private string _BRANCHID;
            private string _ACCTID;
            private string _ACCTTYPE;

            public string BANKACCTFROM
            {
                get { return _BANKACCTFROM; }
                set { _BANKACCTFROM = value; }
            }
            public string BANKID
            {
                get { return _BANKID; }
                set { _BANKID = value; }
            }
            public string BRANCHID
            {
                get { return _BRANCHID; }
                set { _BRANCHID = value; }
            }
            public string ACCTID
            {
                get { return _ACCTID; }
                set { _ACCTID = value; }
            }
            public string ACCTTYPE
            {
                get { return _ACCTTYPE; }
                set { _ACCTTYPE = value; }
            }
        }
        public class STMTTRN
        {
            private string _TRNTYPE;
            private DateTime? _DTPOSTED;
            private DateTime? _DTUSER;
            private DateTime? _DTAVAIL;
            public decimal _TRNAMT;
            private string _FITID;
            private string _NAME;
            private string _MEMO;
            private string _CHECKNUM;
            private string _REFNUM;
            private string _SIC;
            private string _PAYEEID;
            private Payee _PAYEE;

            public string TRNTYPE
            {
                get { return _TRNTYPE; }
                set { _TRNTYPE = value; }
            }
            public DateTime? DTPOSTED
            {
                get { return _DTPOSTED; }
                set { _DTPOSTED = value; }
            }
            public DateTime? DTUSER
            {
                get { return _DTUSER; }
                set { _DTUSER = value; }
            }
            public DateTime? DTAVAIL
            {
                get { return _DTAVAIL; }
                set { _DTAVAIL = value; }
            }

            public decimal TRNAMT
            {
                get { return _TRNAMT; }
                set { _TRNAMT = value; }
            }
            public string FITID
            {
                get { return _FITID; }
                set { _FITID = value; }
            }
            public string NAME
            {
                get { return _NAME; }
                set { _NAME = value; }
            }
            public string MEMO
            {
                get { return _MEMO; }
                set { _MEMO = value; }
            }
            public string CHECKNUM
            {
                get{ return this._CHECKNUM ;}
                set { this._CHECKNUM = value;}
            }
            public string REFNUM
            {
                get{ return this._REFNUM ;}
                set { this._REFNUM = value; }
            }
            public string SIC
            {
                get{ return this._SIC;}
                set { this._SIC = value;}
            }
            public string PAYEEID
            {
                get{ return this._PAYEEID;}
                set { this._PAYEEID = value;}
            }
            public Payee PAYEE
            {
                get
                { 
                    if(this._PAYEE == null)
                        this._PAYEE = new Payee();
                    return this._PAYEE;
                }
                set { }
            }

            public bool HasPayeeInfo ()
            {
                return (this._PAYEE != null);
            }
            
            
        }
        public class BalanceInfo
        {
            private decimal _BALAMT;
            private DateTime? _DTASOF;

            public decimal BALAMT
            {
                get { return _BALAMT; }
                set { _BALAMT = value; }
            }
            public DateTime? DTASOF
            {
                get { return _DTASOF; }
                set { _DTASOF = value; }
            }
        }
        public class Payee
        {
            #region Private Members
		    private string _NAME;
            private string _ADDR1;
            private string _ADDR2;
            private string _ADDR3;
            private string _CITY;
            private string _STATE;
            private string _POSTALCODE;
            private string _COUNTRY;
            private string _PHONE;
            #endregion

            #region Public Props
		    public string NAME 
            { 
                get 
                {
                    return this._NAME; 
                } 
                set
                {
                    this._NAME=value;
                } 
            }
            public string ADDR1 
            { 
                get 
                {
                    return this._ADDR1; 
                } 
                set
                {
                    this._ADDR1=value;
                } 
            }
            public string ADDR2 
            { 
                get 
                {
                    return this._ADDR2; 
                } 
                set
                {
                    this._ADDR2=value;
                } 
            }
            public string ADDR3 
            { 
                get 
                {
                    return this._ADDR3; 
                } 
                set
                {
                    this._ADDR3=value;
                } 
            }
            public string CITY 
            { 
                get 
                {
                    return this._CITY; 
                } 
                set
                {
                    this._CITY=value;
                } 
            }
            public string STATE 
            { 
                get 
                {
                    return this._STATE; 
                } 
                set
                {
                    this._STATE=value;
                } 
            }
            public string POSTALCODE 
            { 
                get 
                {
                    return this._POSTALCODE; 
                } 
                set
                {
                    this._POSTALCODE=value;
                } 
            }
            public string COUNTRY 
            { 
                get 
                {
                    return this._COUNTRY; 
                } 
                set
                {
                    this._COUNTRY=value;
                } 
            }
            public string PHONE 
            { 
                get 
                {
                    return this._PHONE; 
                } 
                set
                {
                    this._PHONE=value;
                } 
            } 
	        #endregion


        }

        public class CCAcctInfo
        {
            private string _ACCTID;
            private string _ACCTKEY;
            
            public string ACCTID
            {
                get { return _ACCTID; }
                set { _ACCTID = value; }
            }
            public string ACCTKEY
            {
                get { return _ACCTKEY; }
                set { _ACCTKEY = value; }
            }
        }

        public class OFXHeader
        {
            public string OFXHEADER;
            public string DATA;
            public string VERSION;
            public string CHARSET;
            public string ENCODING;
            public string COMPRESSION;

            public int MajorVersion
            {
                get { return int.Parse(this.OFXHEADER); }
            }
        }
        #endregion

        #region Parsing-Related Classes
        protected class TagInfo
        {
            public TagInfo(string aName, int aPosition)
            {
                this.tagName = aName;
                this.startPosition = aPosition;
                this.endPosition = this.startPosition;
            }

            public string tagName;
            public int startPosition;
            public int endPosition;
            public bool IsEndPositionValid
            {
                get { return (this.endPosition >= (this.startPosition + this.tagName.Length + 1)); }
            }
            public string OpenTag
            {
                get { return string.Format("<{0}>", this.tagName); }
            }
            public string CloseTag
            {
                get { return string.Format("</{0}>", this.tagName); }
            }
        } 
        #endregion
        #endregion

        #region Ctor + public functions
        public OFXStatementReader()
        {
            #if USE_SGML_PARSER
                this.useConverter = false;
            #else
                this.useConverter = true;
            #endif
        }

        public override void Read(byte[] aInput)
        {
            Encoding inputEncoding = Encoding.GetEncoding(1252);
            string content = inputEncoding.GetString(aInput);
            int ofxIndex = content.IndexOf("<OFX>");
            if (ofxIndex < 0) throw new PXException(Messages.ContentIsNotAValidOFXFile);
            Encoding detectedEncoding;
            ReadHeader(content.Substring(0, ofxIndex), out detectedEncoding);
            if (detectedEncoding.CodePage != inputEncoding.CodePage) 
            {
                content = detectedEncoding.GetString(aInput);
                ofxIndex = content.IndexOf("<OFX>");
            }
            string ofxMessage = content.Substring(ofxIndex);
            ReadOFXMessage(ofxMessage);
        }
        public override bool IsValidInput(byte[] aInput) 
        {
            Encoding inputEncoding = Encoding.GetEncoding(1252);
            int length = aInput.Length <600? aInput.Length: 600;
            string input = inputEncoding.GetString(aInput, 0, length);
            return IsValidInput(input);
        }
		public override bool AllowsMultipleAccounts()
		{
			return true;
		}
		public override void ExportTo(CABankStatementEntry graph, List<CABankStatement> aStatements)
		{
			bool updateCurrent = graph.casetup.Current.ImportToSingleAccount ?? false;
			foreach (OFXStatementReader.STMTRS iAcctStatement in this.Content.Stmtrs)
			{
				this.Export(graph, iAcctStatement, updateCurrent);
				aStatements.Add(graph.BankStatement.Current);
			}
		}
        public STMTTRNRS Content
        {
            get { return content; }
        }

        public bool HasRecords
        {
            get
            {
                return (this.content != null && this.content.Stmtrs.Count > 0);
            }
        }

        #endregion

        #region Private And Internal Functions

        #region Header Readers
        protected virtual void ReadHeader(string aHeader, out Encoding aEncoding)
        {
            int oldFormatIndex = aHeader.IndexOf("OFXHEADER:");
            bool recognized = false;
            if (oldFormatIndex >= 0)
            {
                ReadHeader100(aHeader);
                recognized = true;
            }
            else
            {
                int index = aHeader.IndexOf("<OFX?");
                if (index >= 0)
                {
                    recognized = true;
                    ReadHeader200(aHeader);
                }
            }            
            if (!recognized)
                throw new PXException(Messages.UnknownFormatOfTheOFXHeader);
            aEncoding = this.DetectEncoding();
        }
        protected virtual void ReadHeader100(string aHeader)
        {
            string[] splitters = new string[] { "\r\n","\n","\r"," " }; //File may be formatted for other OS
            string[] splits = aHeader.Split(splitters, StringSplitOptions.RemoveEmptyEntries);            
            foreach (string iSplit in splits)
            {
                string[] valuesSplits = iSplit.Split(':');
                switch (valuesSplits[0].Trim())
                {
                    case "OFXHEADER": this.header.OFXHEADER = valuesSplits[1].Trim(); break;
                    case "VERSION": this.header.VERSION = valuesSplits[1].Trim(); break;
                    case "CHARSET": this.header.CHARSET = valuesSplits[1].Trim(); break;
                    case "ENCODING": this.header.ENCODING = valuesSplits[1].Trim(); break;
                    case "DATA": this.header.DATA = valuesSplits[1].Trim(); break;
                }
            }
        }

        protected Encoding DetectEncoding() 
        {
            if (this.header.ENCODING == "USASCII")
            {
                int charset = int.Parse(this.header.CHARSET);
               return Encoding.GetEncoding(charset);
            }
            else if (this.header.ENCODING == "UNICODE")
            {
                return Encoding.UTF8;
            }
            else
            {
                throw new PXException(Messages.OFXUnsupportedEncodingDetected, this.header.ENCODING, this.header.CHARSET);
            }         
        }

        protected virtual void ReadHeader200(string aHeader)
        {
            this.header.OFXHEADER = "200"; //Atual reader to be written
            this.header.ENCODING = "USASCII";
            this.header.CHARSET = "UNICODE";
        }
        
        #endregion

        #region OFX message reader
        protected virtual void ReadOFXMessage(string aOFXMessage)
        {
            string converted = (this.NeedConvertionToXml() && this.useConverter) ? this.ConvertToXML(aOFXMessage) : aOFXMessage;
            using (XmlReader reader = this.CreateReader(converted))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "STMTTRNRS" || reader.Name == "CCSTMTTRNRS")
                        {
                            OFXStatementReader.Read(reader, this.content, reader.Name);
                        }
                    }
                }
            }
        }
        protected XmlReader CreateReader(string ofxBody)
        {
            XmlReader result = null;
            if (this.header.MajorVersion < 200 && !useConverter)
            {
#if USE_SGML_PARSER
                SgmlReader sgReader = new SgmlReader
                {
                    CaseFolding = CaseFolding.ToUpper,
                    DocType = null,
                    InputStream = new System.IO.StringReader(ofxBody),
                    //SystemLiteral = dtdSystemLiteral,
                    WhitespaceHandling = WhitespaceHandling.None,
                };
                Assembly a = typeof(SgmlReader).Assembly;
                string name = "PX.Objects.CA.ofx160.dtd";
                Stream stm = a.GetManifestResourceStream(name);
                string[] ResRs = a.GetManifestResourceNames();
                if (stm != null)
                {
                    StreamReader sr = new StreamReader(stm);
                    sgReader.Dtd = SgmlDtd.Parse(null, sgReader.DocType, sr, null, sgReader.WebProxy, null);
                }
                result = sgReader;
#endif
            }
            else if (this.header.MajorVersion >= 200 || useConverter)
            {
                result = new XmlTextReader(new System.IO.StringReader(ofxBody));
            }
            return result;
        }
        
        #endregion
        #region SGML-To-XML converter functions
        protected string ConvertToXML(string aSGMLMessage)
        {

            Dictionary<string, Stack<TagInfo>> openTags = new Dictionary<string, Stack<TagInfo>>();
            int index = 0;
            TagInfo lastTag = null;
            while ((index = aSGMLMessage.IndexOf('<', index)) >= 0)
            {
                if (lastTag != null)
                {
                    lastTag.endPosition = index;
                    lastTag = null;
                }
                int endingIndex = aSGMLMessage.IndexOf('>', index);
                if (endingIndex > 0)
                {
                    //Skip self-closing tags
                    if (aSGMLMessage[endingIndex - 1] != '/')
                    {
                        string tag = aSGMLMessage.Substring(index + 1, endingIndex - index - 1);
                        if (tag.Trim().Length > 0)
                        {
                            if (tag[0] != '/')
                            {
                                if (!openTags.ContainsKey(tag))
                                    openTags.Add(tag, new Stack<TagInfo>());
                                lastTag = new TagInfo(tag, index);
                                openTags[tag].Push(lastTag);
                            }
                            else
                            {
                                tag = tag.Substring(1);
                                if (openTags.ContainsKey(tag))
                                {
                                    TagInfo last = openTags[tag].Pop();
                                    lastTag = null;
                                }
                            }
                        }
                    }
                    index = endingIndex + 1;
                }
                else
                {
                    throw new PXException(Messages.OFXDocumentHasUnclosedTag , index);
                }
            }
            SortedDictionary<int, TagInfo> unclosedTags = new SortedDictionary<int, TagInfo>();
            int additionalLength = 0;
            foreach (KeyValuePair<string, Stack<TagInfo>> it in openTags)
            {
                foreach (TagInfo iTag in it.Value)
                {
                    unclosedTags.Add(iTag.startPosition, iTag);
                    additionalLength += (iTag.tagName.Length + 3); // Length of closing tag </TagName>
                }
            }
            StringBuilder result = new StringBuilder(aSGMLMessage.Length + additionalLength);
            int lastPosition = 0;
            foreach (KeyValuePair<int, TagInfo> iTag in unclosedTags)
            {
                TagInfo tagInfo = iTag.Value;
                if (tagInfo.IsEndPositionValid == false)
                {
                    tagInfo.endPosition = aSGMLMessage.IndexOf("<", tagInfo.startPosition + tagInfo.OpenTag.Length);
                }
                int insertPoint = tagInfo.endPosition;
                string end = aSGMLMessage.Substring(insertPoint - 2, 2);
                if (end == "\r\n")
                    insertPoint -= 2;
                result.Append(aSGMLMessage.Substring(lastPosition, insertPoint - lastPosition));
                result.Append(tagInfo.CloseTag);
                lastPosition = insertPoint;
            }
            if (lastPosition < aSGMLMessage.Length)
            {
                result.Append(aSGMLMessage.Substring(lastPosition));
            }
            return result.ToString();
        }
      
        protected virtual bool NeedConvertionToXml()
        {
            return (this.header.MajorVersion < 200);
        }
        
        #endregion
   
		protected static bool IsValidInput(string aInput)
		{
			int ofxIndex = aInput.IndexOf("<OFX>");
			return ofxIndex > 0;
		}
		protected void Export(CABankStatementEntry aGraph, STMTRS aAcctStatement, bool updateCurrent) 
		{
			if (!aAcctStatement.HasAccountInfo())
				throw new PXException(Messages.OFXImportErrorAccountInfoIsIncorrect);
			
			CashAccount acct = aGraph.cashAccountByExtRef.Select(aAcctStatement.IsBankAccount() ? aAcctStatement.BANKACCTFROM.ACCTID : aAcctStatement.CCACCTFROM.ACCTID);
			if (acct == null) throw new PXException(Messages.CashAccountWithExtRefNbrIsNotFoundInTheSystem, aAcctStatement.BANKACCTFROM.ACCTID);
			if (aGraph.casetup.Current.IgnoreCuryCheckOnImport != true && acct.CuryID != aAcctStatement.CURDEF)
                throw new PXException(Messages.CashAccountHasCurrencyDifferentFromOneInStatement, acct.CashAccountCD, acct.CuryID, aAcctStatement.CURDEF);

			foreach (OFXStatementReader.STMTTRN iTran in aAcctStatement.BANKTRANLIST.Trans)
			{
				string stmtRefNbr;
				if (aGraph.IsAlreadyImported(acct.CashAccountID, iTran.FITID, out stmtRefNbr))
				{
                    throw new PXException(Messages.TransactionWithFitIdHasAlreadyBeenImported, iTran.FITID, stmtRefNbr, acct.CashAccountCD, acct.Descr);
				}
			}

			CABankStatement statement = null;
			if (updateCurrent == false || aGraph.BankStatement.Current == null || aGraph.BankStatement.Current.CashAccountID == null)
			{
				aGraph.Clear();
				statement = new CABankStatement();
				statement.CashAccountID = acct.CashAccountID;
				statement = aGraph.BankStatement.Insert(statement);
				aGraph.BankStatement.Current = statement;
			}
			else
			{
				statement = aGraph.BankStatement.Current ;
				if (statement.CashAccountID.HasValue)
				{
					if (statement.CashAccountID != acct.CashAccountID)
					{
                        throw new PXException(Messages.ImportedStatementIsMadeForAnotherAccount, acct.CashAccountCD, acct.Descr);
					}
				}				
			}
			CABankStatement copy = (CABankStatement)aGraph.BankStatement.Cache.CreateCopy(statement);
			Copy(copy, aAcctStatement);
			statement = aGraph.BankStatement.Update(copy);
			foreach (OFXStatementReader.STMTTRN iTran in aAcctStatement.BANKTRANLIST.Trans)
			{
				CABankStatementDetail detail = new CABankStatementDetail();
				Copy(detail,iTran);				
				detail = aGraph.Details.Insert(detail);
				CABankStatementDetail detail1 = (CABankStatementDetail)aGraph.Details.Cache.CreateCopy(detail);
				//Must be done after to avoid overwriting of debit by credit
				CopyForUpdate(detail1, iTran);
				detail = aGraph.Details.Update(detail1);
			}
			aGraph.Save.Press();
		}
		protected static void Copy(CABankStatement aDest, STMTRS aSrc) 
		{
			aDest.BankStatementFormat = "OFX";
			aDest.StartBalanceDate = aSrc.BANKTRANLIST.DTSTART;
			aDest.EndBalanceDate = aSrc.BANKTRANLIST.DTEND;
			aDest.CuryEndBalance = aSrc.LEDGERBAL.BALAMT;
		}
		protected static void Copy(CABankStatementDetail aDest, STMTTRN aSrc)
		{
			aDest.TranCode = aSrc.TRNTYPE;
			aDest.TranDate = aSrc.DTPOSTED;
			aDest.ExtTranID = aSrc.FITID;
			aDest.ExtRefNbr = (string.IsNullOrEmpty(aSrc.CHECKNUM) == false) ? aSrc.CHECKNUM
				: (string.IsNullOrEmpty(aSrc.REFNUM) == false && aSrc.REFNUM.Length < 15 ? aSrc.REFNUM : (aSrc.FITID.Length < 15 ? aSrc.FITID : null));
			aDest.TranDesc = aSrc.NAME + " " + aSrc.MEMO;
			if (aSrc.HasPayeeInfo())
			{
				aDest.PayeeName = aSrc.PAYEE.NAME;
				aDest.PayeeAddress1 = aSrc.PAYEE.ADDR1;
				aDest.PayeePostalCode = aSrc.PAYEE.POSTALCODE;
				aDest.PayeePhone = aSrc.PAYEE.PHONE;
				aDest.PayeeState = aSrc.PAYEE.STATE;
				aDest.PayeeCity = aSrc.PAYEE.CITY;
			}
			else
			{
				aDest.PayeeName = aSrc.NAME;
			}
		}
		protected static void CopyForUpdate(CABankStatementDetail aDest, STMTTRN aSrc)
		{
			if (aSrc.TRNAMT >= 0)
			{
				aDest.CuryDebitAmt = aSrc.TRNAMT;
			}
			else
			{
				aDest.CuryCreditAmt = -aSrc.TRNAMT;
			}
		}
		#endregion
		#region Protected Static Functions
		#region Reader Functions
		protected static void Read(XmlReader reader, STMTTRNRS aTranList, string sectionTag)
        {
            string lastName = string.Empty;            
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                    if (reader.Name == "STMTRS" || reader.Name == "CCSTMTRS")
                    {
                        STMTRS acctTrans = new STMTRS();
                        aTranList.Stmtrs.Add(acctTrans);
                        Read(reader, acctTrans, reader.Name);
                    }
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    if (lastName == "TRNUID")
                    {
                        aTranList.TRNUID = reader.Value.Trim();
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && (reader.Name == sectionTag))
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, STMTRS aTranList, string sectionTag)
        {
            Type type = aTranList.GetType();
            PropertyInfo[] members = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo iMem in members)
            {
                string name = iMem.Name;
                Type tp = iMem.PropertyType;
            }
            string lastName = string.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                    if (reader.Name == "BANKACCTFROM")
                    {
                        Read(reader, aTranList.BANKACCTFROM);
                    }

                    if(reader.Name == "CCACCTFROM")
                    {
                        Read(reader, aTranList.CCACCTFROM);
                    }

                    if (reader.Name == "BANKTRANLIST")
                    {
                        Read(reader, aTranList.BANKTRANLIST);
                    }

                    if (lastName == "LEDGERBAL")
                        Read(reader, aTranList.LEDGERBAL, "LEDGERBAL");
                    if (lastName == "AVAILBAL")
                        Read(reader, aTranList.AVAILBAL, "AVAILBAL");
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    if (lastName == "CURDEF")
                    {
                        aTranList.CURDEF = reader.Value.Trim();
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && (reader.Name == sectionTag))
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, BankAcctInfo aInfo)
        {
            string lastName = String.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string value = reader.Value.Trim();
                    switch (lastName)
                    {
                        case "BANKID": aInfo.BANKID = value; break;
                        case "ACCTID": aInfo.ACCTID = value; break;
                        case "ACCTTYPE": aInfo.ACCTTYPE = value; break;
                        case "BRANCHID": aInfo.BRANCHID = value; break;
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "BANKACCTFROM")
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, CCAcctInfo aInfo)
        {
            string lastName = String.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string value = reader.Value.Trim();
                    switch (lastName)
                    {
                        case "ACCTID": aInfo.ACCTID = value; break;
                        case "ACCTKEY": aInfo.ACCTKEY= value; break;                        
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "CCACCTFROM")
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, BankTransList aTransList)
        {
            string lastName = String.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                    if (lastName == "STMTTRN")
                    {
                        STMTTRN tran = new STMTTRN();
                        aTransList.Trans.Add(tran);
                        Read(reader, tran);
                    }
                }

                if (reader.NodeType == XmlNodeType.Text)
                {
                    try
                    {
                        if (lastName == "DTSTART")
                        {
                            string value = reader.Value.Trim();
                            aTransList.DTSTART = ParseDateTime(value); 
                        }
                        if (lastName == "DTEND")
                        {
                            string value = reader.Value.Trim();
                            aTransList.DTEND = ParseDateTime(value); 
                        }
                    }
                    catch (FormatException ex)
                    {
                        throw new PXException(Messages.OFXParsingErrorValueHasInvalidFormat, lastName, ex.Message);
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "BANKTRANLIST")
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, STMTTRN aTran)
        {
            string lastName = String.Empty;
            Type type = aTran.GetType();
            PropertyInfo[] members = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo iMem in members)
            {
                string name = iMem.Name;
                Type tp = iMem.PropertyType;
            }
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                    if(reader.Name == "PAYEE")
                    {
                        Read(reader,aTran.PAYEE);
                    }
                }
                try
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (lastName == "DTPOSTED")
                        {
                            string value = reader.Value.Trim();
                            aTran.DTPOSTED = ParseDateTime(value); 
                        }
                        if (lastName == "DTUSER")
                        {
                            string value = reader.Value.Trim();
                            aTran.DTUSER = ParseDateTime(value); 
                        }
                        if (lastName == "DTAVAIL")
                        {
                            string value = reader.Value.Trim();
                            aTran.DTAVAIL = ParseDateTime(value);
                        }

                        if (lastName == "TRNTYPE")
                        {
                            aTran.TRNTYPE = reader.Value.Trim();
                        }
                        if (lastName == "FITID")
                        {
                            aTran.FITID = reader.Value.Trim();
                        }
                        if (lastName == "NAME")
                        {
                            aTran.NAME = reader.Value.Trim();
                        }
                        if (lastName == "MEMO")
                        {
                            aTran.MEMO = reader.Value.Trim();
                        }
                        if (lastName == "CHECKNUM")
                        {
                            aTran.CHECKNUM = reader.Value.Trim();
                        }
                        if (lastName == "REFNUM")
                        {
                            aTran.CHECKNUM = reader.Value.Trim();
                        }
                        if (lastName == "SIC")
                        {
                            aTran.CHECKNUM = reader.Value.Trim();
                        }
                        if (lastName == "PAYEEID")
                        {
                            aTran.CHECKNUM = reader.Value.Trim();
                        }
                        if (lastName == "TRNAMT")
                        {
                            aTran.TRNAMT = ParseAmount(reader.Value.Trim());
                        }                        
                    }
                }
                catch (FormatException ex)
                {
                     throw new PXException(Messages.OFXParsingErrorTransactionValueHasInvalidFormat, reader.Value, lastName, aTran.FITID, ex.Message);
                }               

                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "STMTTRN")
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, Payee aPayee)
        {
            string lastName = String.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    if (lastName == "NAME")
                    {                       
                        aPayee.NAME = reader.Value.Trim();
                    }
                    if (lastName == "ADDR1")
                    {
                        aPayee.ADDR1 = reader.Value.Trim();
                    }
                    if (lastName == "ADDR2")
                    {
                        aPayee.ADDR2= reader.Value.Trim();
                    }
                    if (lastName == "ADDR3")
                    {
                        aPayee.ADDR2= reader.Value.Trim();
                    }

                    if (lastName == "CITY")
                    {
                        aPayee.NAME = reader.Value.Trim();
                    }
                    if (lastName == "POSTALCODE")
                    {
                        aPayee.POSTALCODE = reader.Value.Trim();
                    }
                    if (lastName == "COUNTRY")
                    {
                        aPayee.COUNTRY = reader.Value.Trim();
                    }
                    if (lastName == "PHONE")
                    {
                        aPayee.COUNTRY = reader.Value.Trim();
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "PAYEE")
                {
                    return;
                }
            }
        }

        protected static void Read(XmlReader reader, BalanceInfo aInfo, string aEndTag)
        {
            string lastName = String.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string value = reader.Value.Trim();
                    try
                    {
                        switch (lastName)
                        {
                            case "BALAMT": aInfo.BALAMT = ParseAmount(value); break;
                            case "DTASOF": aInfo.DTASOF = ParseDateTime(value); break;
                        }
                    }
                    catch (FormatException ex)
                    {
                        throw new PXException(Messages.OFXParsingErrorValueHasInvalidFormat, lastName,ex.Message);
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == aEndTag)
                {
                    return;
                }
            }
        }

        //Generic reading function - is not finished
        protected static void Read(XmlReader reader, object aTarget, string sectionTag)
        {
            Type type = aTarget.GetType();
            PropertyInfo[] members = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Dictionary<string, PropertyInfo> complexTypes = new Dictionary<string, PropertyInfo>();
            Dictionary<string, PropertyInfo> basicTypes = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo iMem in members)
            {
                string name = iMem.Name;
                Type tp = iMem.PropertyType;
                if (tp.IsValueType || tp == typeof(string))
                {
                    basicTypes.Add(name, iMem);
                }
                else
                {
                    if (tp.IsClass)
                    {
                        complexTypes.Add(name, iMem);
                    }
                }
            }
            string lastName = string.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    lastName = reader.Name;
                    if (complexTypes.ContainsKey(lastName))
                    {
                        Read(reader, (complexTypes[lastName].GetValue(aTarget, null)), lastName);
                    }
                }
                if (reader.NodeType == XmlNodeType.Text)
                {
                    if (basicTypes.ContainsKey(lastName))
                    {
                        PropertyInfo prop = basicTypes[lastName];

                        bool isDateTime = prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime);
                        if (isDateTime)
                        {
                            prop.SetValue(aTarget, ParseDateTime(reader.Value), null);
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(aTarget, reader.Value, null);
                        }
                        else
                        {
                            MethodInfo parseMethod = prop.PropertyType.GetMethod("Parse", BindingFlags.Static);
                            if (parseMethod != null)
                            {
                                Object[] args = { reader.Value };
                                prop.SetValue(aTarget, parseMethod.Invoke(null, args), null);
                            }
                        }

                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement && (reader.Name == sectionTag))
                {
                    return;
                }
            }
        }

        #endregion
        #region Parsing Function
        protected static DateTime? ParseDateTime(string aDateAsString)
        {
            DateTime? result = null;
            bool useZoneInfo = false;
            int timeZoneIndex = aDateAsString.IndexOf('[');
            bool hasTimeZone = timeZoneIndex > 0;
            TimeZoneInfo tzInfo = null;
            string dateTime = String.Empty;
            if (!hasTimeZone)
            {
                dateTime = aDateAsString;
            }
            else
            {
                dateTime = aDateAsString.Substring(0, timeZoneIndex);
                int rightIndex = aDateAsString.IndexOf(']');
                string timeZone = aDateAsString.Substring(timeZoneIndex + 1, rightIndex - timeZoneIndex - 1);
                string[] timeZoneParts = timeZone.Split(':');
                string offset = timeZoneParts[0];
                bool useOffset = true;
                if (useZoneInfo && timeZoneParts.Length > 1)
                {
                    useOffset = false;
                    string timeZoneName = timeZoneParts[1];                    
                    try
                    {
                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        useOffset = true;
                    }
                    catch (InvalidTimeZoneException)
                    {
                        useOffset = true;
                    }
                }
                if (useOffset)
                {
                    string[] parts = offset.Split('.');
                    int[] fractions = { 0, 0 };
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (i < 2)
                        {
                            fractions[i] = int.Parse(parts[i]);
                        }
                    }
                    dateTime = String.Format("{0} {1:+00;-00;+00}:{2:00}", dateTime, fractions[0], fractions[1]);
                }
            }
            string[] formats = { "yyyyMMdd", "yyyyMMddHHmmss", "yyyyMMddHHmmss.fff" };
            if (dateTime.Length == 8 || dateTime.Length == 14 || dateTime.Length == 18)
            {
                result = DateTime.ParseExact(dateTime, formats, null, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }
            else
            {
                string[] offsetFormats = { "yyyyMMdd zzz", "yyyyMMddHHmmss zzz", "yyyyMMddHHmmss.fff zzz" };
                DateTimeOffset dt = DateTimeOffset.ParseExact(dateTime, offsetFormats, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
                result = dt.DateTime;
            }
            if (tzInfo != null)
                return TimeZoneInfo.ConvertTimeToUtc(result.Value, tzInfo);
            return result;
        }
        protected static decimal ParseAmount(string aNumber)
        {
            decimal result;
            const string comma = ",";
            bool hasPoint = aNumber.IndexOf(".")>=0;
            bool hasComma = aNumber.IndexOf(comma)>=0;
            if (hasPoint)
            {
                result = decimal.Parse(aNumber, NumberStyles.Number);
            }
            else 
            {
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                nfi.CurrencyDecimalSeparator = comma;
                nfi.NumberDecimalSeparator = comma;                
                result = decimal.Parse(aNumber, (NumberStyles.Number &(~NumberStyles.AllowThousands)), nfi);
            }
            return result;
        }
        #endregion
        #endregion
    }

}
