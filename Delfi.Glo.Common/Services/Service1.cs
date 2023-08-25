namespace Delfi.Glo.Common.Services
{
    // add common services here in this folder
    public sealed class Service1 : IAsyncDisposable, IDisposable
    {
        private readonly StreamWriter _streamWriter;

        public Service1(Stream stream)
        {
            _streamWriter = new StreamWriter(stream);
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _streamWriter.DisposeAsync();
        }
    }
}
