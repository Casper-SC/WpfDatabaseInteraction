using System.Collections.Generic;

namespace DatabaseExample.Services
{
    public class DatabaseServiceUpdateEventArgs<T>
    {
        public DatabaseServiceUpdate UpdateInfo { get; }

        public IList<T> Data { get; }

        public DatabaseServiceUpdateEventArgs(T data, DatabaseServiceUpdate updateInfo)
            : this(new List<T> { data }, updateInfo)
        {
        }

        public DatabaseServiceUpdateEventArgs(IList<T> data, DatabaseServiceUpdate updateInfo)
        {
            UpdateInfo = updateInfo;
            Data = data;
        }
    }
}