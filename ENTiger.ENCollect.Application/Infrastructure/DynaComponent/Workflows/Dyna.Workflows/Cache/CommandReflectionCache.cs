using ENCollect.Dyna.Workflows;
using ENTiger.ENCollect;
using Sumeru.Flex;
using System;
using System.Collections.Concurrent;
using System.Reflection;

/// <summary>
/// Creates domain commands declared in <c>ActionCommandMap</c>
/// and populates <see cref="IDynaWorkflowDomainCommand{TDto}"/> fields.
/// A tiny cache avoids repeated Activator calls under load.
/// </summary>
public static class CommandReflectionCache
{
    // -----------------------------------------------------------------------------
    private static readonly ConcurrentDictionary<Type, 
        Func<string, string, string, string, string, object, object>>
        _factoryCache = new();

    // -----------------------------------------------------------------------------
    // Creates a domain command and wires the common fields *plus* the optional
    // IDataPacket coming from WorkflowTransitionCommand.
    // -----------------------------------------------------------------------------
    public static object CreateAndPopulate(
    Type commandType,
    string domainId, // Changed from string to Guid
    string userId, 
    string workflowInstanceId,
    string stepName,
    string stepType,
    object dto)
    {
        // Contract guard
        //if (!commandType.IsGenericType || commandType.GetGenericTypeDefinition() != typeof(IDynaWorkflowDomainCommand))
        //    throw new InvalidOperationException(
        //        $"Command type {commandType.Name} must implement IDynaWorkflowDomainCommand<TDto>.");

        // fast-path: cached lambda (Guid, string, IDataPacket?) -> object
        var factory = _factoryCache.GetOrAdd(commandType, t =>
        {
            // Use reflection to create a delegate that matches the expected return type
            return (Func<string, string, string, string, string, object, object>)
            ((id, usr, wId, sName, sType, dto) =>
            {
                var inst = Activator.CreateInstance(t)!;
                var domainCommand = inst as IDynaWorkflowDomainCommand;
                if (domainCommand == null)
                    throw new InvalidOperationException($"Instance of type {t.Name} does not implement IDynaWorkflowDomainCommand<TDto>.");

                domainCommand.DomainId = id.ToString(); // Convert Guid to string
                domainCommand.UserId = usr;               
                domainCommand.Dto = dto;
                domainCommand.WorkflowInstanceId = wId;
                domainCommand.StepName = sName;
                domainCommand.StepType = sType;

                return domainCommand;
            });
        });

        return factory(domainId, userId, workflowInstanceId,stepName, stepType, dto);
    }
}
