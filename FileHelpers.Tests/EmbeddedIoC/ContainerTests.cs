using NUnit.Framework;
using Singleton = FileHelpers.Container.SingletonAttribute;

namespace FileHelpers.Tests.IoC
{
    #region Demo Contracts and Implementations

    public interface IUnimplementedContract
    {
    }

    public interface IUnimplementedDependentContract
    {
    }

    internal class UnimplementedDependentContract : IUnimplementedDependentContract
    {
        public UnimplementedDependentContract(IUnimplementedContract dependency)
        {

        }
    }

    public interface IImplementedContract
    {
    }

    public class ImplementedContract : IImplementedContract
    {
    }

    public interface IAnotherImplementedContract
    {
    }

    public class AnotherImplementedContract : IAnotherImplementedContract
    {
    }

    public interface IImplementedDependentContract
    {
        IImplementedContract Dependency1 { get; }
        IAnotherImplementedContract Dependency2 { get; }
    }

    public class ImplementedDependentContract : IImplementedDependentContract
    {
        public IImplementedContract Dependency1 { get; private set; }
        public IAnotherImplementedContract Dependency2 { get; private set; }

        public ImplementedDependentContract(IUnimplementedContract thisConstructorShouldNotBeLookedUpByContainer)
        {
            Assert.Fail("Wrong constructor resolved");
        }

        private ImplementedDependentContract(IImplementedContract dependency1, IAnotherImplementedContract dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }
    }
    
    public interface IImplementedContractWithNonContainerConstructorArgs
    {
        string Arg { get; }
    }

    public class ImplementedContractWithNonContainerConstructorArgs : IImplementedContractWithNonContainerConstructorArgs
    {
        public string Arg { get; private set; }
        public ImplementedContractWithNonContainerConstructorArgs(string arg)
        {
            Arg = arg;
        }
    }

    public interface ISingletonContract
    {
        
    }

    [Container.SingletonAttribute]
    public class SingletonContract : ISingletonContract
    {
        
    }

    #endregion

    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void Resolve_should_throw_when_no_implementation_found_for_contract()
        {
            Assert.Throws<Container.TypeResolutionException>(() => Container.Resolve<IUnimplementedContract>());
        }

        [Test]
        public void Resolve_should_throw_when_no_implementation_found_for_dependent_contract()
        {
            Assert.Throws<Container.TypeResolutionException>(() => Container.Resolve<IUnimplementedDependentContract>());
        }

        [Test]
        public void Resolve_should_return_instance_of_implementation_when_implementation_found_for_contract()
        {
            Assert.That(Container.Resolve<IImplementedContract>(), Is.InstanceOf<ImplementedContract>());
        }

        [Test]
        public void Resolve_should_return_new_instance_of_implementation_when_implementation_not_marked_as_Singleton()
        {
            var instance1 = Container.Resolve<IImplementedContract>();
            var instance2 = Container.Resolve<IImplementedContract>();
            Assert.That(instance1, Is.InstanceOf<ImplementedContract>());
            Assert.That(instance2, Is.InstanceOf<ImplementedContract>());
            Assert.That(instance1, Is.Not.SameAs(instance2));
        }

        [Test]
        public void Resolve_should_return_same_instance_of_implementation_when_implementation_is_marked_as_Singleton()
        {
            var instance1 = Container.Resolve<ISingletonContract>();
            var instance2 = Container.Resolve<ISingletonContract>();
            Assert.That(instance1, Is.InstanceOf<SingletonContract>());
            Assert.That(instance2, Is.InstanceOf<SingletonContract>());
            Assert.That(instance1, Is.SameAs(instance2));
        }

        [Test]
        // TODO: break apart. This test also implicitly proves that longest ctor is selected as well as private ctors can be selected.
        public void Resolve_should_return_instance_of_implementation_when_implementation_found_for_contract_and_all_dependent_contracts()
        {
            var toplevel = Container.Resolve<IImplementedDependentContract>();
            Assert.That(toplevel, Is.InstanceOf<ImplementedDependentContract>());
            Assert.That(toplevel.Dependency1, Is.InstanceOf<IImplementedContract>());
            Assert.That(toplevel.Dependency2, Is.InstanceOf<IAnotherImplementedContract>());
        }

        [Test]
        public void Resolve_with_args_should_return_instance_of_implementation_when_implementation_found_for_contract()
        {
            var contract = Container.Resolve<IImplementedContractWithNonContainerConstructorArgs>("string arg");
            Assert.That(contract, Is.InstanceOf<ImplementedContractWithNonContainerConstructorArgs>());
            Assert.That(contract.Arg, Is.EqualTo("string arg"));
        }

        [Test]
        public void Resolve_with_args_should_throw_when_too_many_args_given()
        {
            Assert.Throws<Container.TypeResolutionException>(() =>Container.Resolve<IImplementedContract>("foo"));
        }
    }
}