using System.Collections.Generic;
namespace System.Management
{
    /// <summary>
    /// Acts as a broker for managing a single WQL query and subsequent executions.
    /// </summary>
    public class ManagementQueryBroker
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of System.Management.ManagementQueryBroker.
        /// </summary>
        /// <param name="query">The WQL Query to be executed.</param>
        /// <param name="namespacePath">Namespace path of the ManagementPath within which to execute a query.</param>
        /// <param name="connectionOptions">Specifies all settings required to make a WMI connection.</param>
        public ManagementQueryBroker(String query, ManagementScope scope)
        {
            this.query = query;
            this.scope = scope;
            this.queryType = query.GetWqlQueryType();
            this.results = new List<ManagementBaseObject>();

            this.queryObserver = new ManagementOperationObserver();
            this.queryObserver.ObjectReady += new ObjectReadyEventHandler(OnObjectReady);
            this.queryObserver.Completed += new CompletedEventHandler(OnQueryCompleted);
        }

        #endregion

        #region Fields

        private String query;

        private WqlQueryType queryType;

        private ManagementScope scope;

        private ManagementObjectSearcher querySearcher;

        private ManagementOperationObserver queryObserver;

        private Int32 resultCount;

        private Boolean inProgress;

        private List<ManagementBaseObject> results;

        private ManagementClass resultClass;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the WqlQueryType of the WQL Query string.
        /// </summary>
        public WqlQueryType QueryType
        {
            get { return this.queryType; }
        }

        /// <summary>
        /// Gets a value reflecting whether query execution is in progress.
        /// </summary>
        public Boolean InProgress
        {
            get { return this.inProgress; }
        }

        /// <summary>
        /// Gets the number of ManagementObject results returned so far.
        /// </summary>
        public Int32 ResultCount
        {
            get { return this.resultCount; }
        }

        /// <summary>
        /// Gets the list of currently returned query results.
        /// </summary>
        public List<ManagementBaseObject> Results
        {
            get { return this.results; }
        }

        public ManagementClass ResultClass
        {
            get
            {
                if (this.resultCount < 1)
                    return null;

                if (this.resultClass == null)
                {
                    this.resultClass = new ManagementClass(this.scope, this.results[0].ClassPath, new ObjectGetOptions());
                }

                return this.resultClass;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the WQL query asynchronously.
        /// </summary>
        public void ExecuteAsync()
        {
            this.OnStarted(this, EventArgs.Empty);

            try
            {
                this.querySearcher = new ManagementObjectSearcher(this.scope, new ObjectQuery(query));
                this.querySearcher.Get(this.queryObserver);
            }

            catch (ManagementException)
            {
                this.inProgress = false;
            }
        }

        /// <summary>
        /// Cancels asynchronous execution of the WQL Query.
        /// </summary>
        public void Cancel()
        {
            this.queryObserver.Cancel();
            this.querySearcher.Dispose();

            // ? this.OnQueryCompleted(this, EventArgs.Empty);
        }

        protected virtual void OnStarted(object sender, EventArgs e)
        {
            this.resultCount = 0;
            this.results.Clear();

            if (this.resultClass != null)
                this.resultClass.Dispose();
            this.resultClass = null;

            this.inProgress = true;

            if (this.Started != null)
                this.Started(sender, e);
        }

        protected virtual void OnQueryCompleted(object sender, CompletedEventArgs e)
        {
            this.inProgress = false;
            if (null != this.Completed)
                this.Completed(sender, e);
        }

        protected virtual void OnObjectReady(object sender, ObjectReadyEventArgs e)
        {
            this.resultCount++;
            this.results.Add(e.NewObject);
            
            if (null != this.ObjectReady)
                this.ObjectReady(sender, e);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when an operation has started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Occurs when a new object is available.
        /// </summary>
        public event ObjectReadyEventHandler ObjectReady;

        /// <summary>
        /// Occurs when an operation has completed.
        /// </summary>
        public event CompletedEventHandler Completed;

        #endregion
    }
}
