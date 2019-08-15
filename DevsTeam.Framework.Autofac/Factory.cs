namespace DevsTeam.Framework.Autofac
{
    public delegate Disposable<TResult> Factory<in TParameter, TResult>(TParameter parameter) where TParameter : class;
}