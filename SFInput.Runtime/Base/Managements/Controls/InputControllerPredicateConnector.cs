using System.Collections.Generic;

namespace SFInput {
public sealed class InputControllerPredicateConnector<TController> where TController : IInputController
{
    private readonly IInputControllerManager<TController> _controllerManager;
    private readonly List<ConnectionPredicate> _connectionPredicates;

    public InputControllerPredicateConnector(IInputControllerManager<TController> manager)
    {
        _controllerManager = manager;
        _connectionPredicates = new();
    }

    public void Disable()
    {
        _controllerManager.ControllerAdded -= AddControllerConnection;
        _controllerManager.ControllerRemoved -= RemoveControllerConnection;
        _controllerManager.PredicateManager.PredicateAdded -= AddPredicateConnection;
        _controllerManager.PredicateManager.PredicateRemoved -= RemovePredicateConnection;
    }

    public void Enable()
    {
        _controllerManager.ControllerAdded += AddControllerConnection;
        _controllerManager.ControllerRemoved += RemoveControllerConnection;
        _controllerManager.PredicateManager.PredicateAdded += AddPredicateConnection;
        _controllerManager.PredicateManager.PredicateRemoved += RemovePredicateConnection;
    }

    public void AddControllerConnection(int index, TController controller)
    {
        foreach (var predicate in _controllerManager.PredicateManager.Predicates)
        {
            SubscribeController(controller, predicate);
        }
    }

    public void RemoveControllerConnection(int index, TController controller)
    {
        foreach (var predicate in _controllerManager.PredicateManager.Predicates)
        {
            UnsubscribeController(controller, predicate);
        }
    }

    public void AddPredicateConnection(IInputPredicate<TController> predicate)
    {
        foreach (var controllers in _controllerManager.Controllers.Values)
        {
            foreach (var controller in controllers)
            {
                SubscribeController(controller, predicate);
            }
        }
    }

    public void RemovePredicateConnection(IInputPredicate<TController> predicate)
    {
        foreach (var controllers in _controllerManager.Controllers.Values)
        {
            foreach (var controller in controllers)
            {
                UnsubscribeController(controller, predicate);
            }
        }
    }

    public void SubscribeController(TController controller, IInputPredicate<TController> predicate)
    {
        var connectionPredicate = new ConnectionPredicate(controller, predicate);

        if (controller.PredicateManager.AddPredicate(connectionPredicate))
        {
            _connectionPredicates.Add(connectionPredicate);
        }
    }

    public void UnsubscribeController(TController controller, IInputPredicate<TController> predicate)
    {
        var connectionPredicate = _connectionPredicates.Find(x => x.Controller.Equals(controller) && x.Predicate == predicate);

        if (connectionPredicate is not null)
        {
            if (controller.PredicateManager.RemovePredicate(connectionPredicate))
            {
                _connectionPredicates.Remove(connectionPredicate);
            }
        }
    }

    private sealed class ConnectionPredicate : IInputPredicate
    {
        public readonly TController Controller;
        public readonly IInputPredicate<TController> Predicate;

        public ConnectionPredicate(TController controller, IInputPredicate<TController> predicate)
        {
            Controller = controller;
            Predicate = predicate;
        }

        public bool Result()
        {
            return Predicate.Result(Controller);
        }
    }
}}