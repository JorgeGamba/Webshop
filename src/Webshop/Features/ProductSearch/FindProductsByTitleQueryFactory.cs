namespace Webshop.Features.ProductSearch
{
    public class FindProductsByTitleQueryFactory
    {
        private readonly InMemoryStorage _inMemoryStorage;

        public FindProductsByTitleQueryFactory(InMemoryStorage inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
        }

        public IFindProductsByTitleQuery Create(bool workingOnMemory)
        {
            if (workingOnMemory)
                return new FindProductsByTitleMemoryQuery(_inMemoryStorage);

            return new FindProductsByTitleDbQuery();
        }
    }
}