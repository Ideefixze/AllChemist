namespace AllChemist.Serialization
{
    /*
     * Returns some kind of data.
     */
    interface IJSONDeserializer<T>
    {
        public T GetData();
    }
}
