namespace Genesis.data
{
    public class BaseDto<T>
    {
        public int status { get; set; }
        public string message { get; set; } = default!;
        public T data { get; set; } = default!;

    }
}
