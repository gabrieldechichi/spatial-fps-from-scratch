// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using Unity.Entities;
using Improbable.Worker.CInterop;
using Improbable.Gdk.Core;

namespace Improbable.Gdk.Health
{
    public partial class HealthRegenComponent
    {
        public class EcsViewManager : IEcsViewManager
        {
            private WorkerSystem workerSystem;
            private EntityManager entityManager;
            private World world;

            private readonly ComponentType[] initialComponents = new ComponentType[]
            {
                ComponentType.Create<Component>(),
                ComponentType.Create<ComponentAuthority>(),
            };

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public ComponentType[] GetInitialComponents()
            {
                return initialComponents;
            }

            public void ApplyDiff(ViewDiff diff)
            {
                var diffStorage = (DiffComponentStorage) diff.GetComponentDiffStorage(ComponentId);

                foreach (var entityId in diffStorage.GetComponentsAdded())
                {
                    AddComponent(entityId);
                }

                var updates = diffStorage.GetUpdates();
                var dataFromEntity = workerSystem.GetComponentDataFromEntity<Component>();
                for (int i = 0; i < updates.Count; ++i)
                {
                    ApplyUpdate(in updates[i], dataFromEntity);
                }

                var authChanges = diffStorage.GetAuthorityChanges();
                for (int i = 0; i < authChanges.Count; ++i)
                {
                    ref readonly var change = ref authChanges[i];
                    SetAuthority(change.EntityId, change.Authority);
                }

                foreach (var entityId in diffStorage.GetComponentsRemoved())
                {
                    RemoveComponent(entityId);
                }
            }

            public void Init(World world)
            {
                this.world = world;
                entityManager = world.GetOrCreateManager<EntityManager>();

                workerSystem = world.GetExistingManager<WorkerSystem>();

                if (workerSystem == null)
                {
                    throw new ArgumentException("World instance is not running a valid SpatialOS worker");
                }
            }

            public void Clean(World world)
            {
            }

            private void AddComponent(EntityId entityId)
            {
                workerSystem.TryGetEntity(entityId, out var entity);

                var component = new Improbable.Gdk.Health.HealthRegenComponent.Component();

                component.MarkDataClean();
                entityManager.AddSharedComponentData(entity, ComponentAuthority.NotAuthoritative);
                entityManager.AddComponentData(entity, component);
            }

            private void RemoveComponent(EntityId entityId)
            {
                workerSystem.TryGetEntity(entityId, out var entity);
                entityManager.RemoveComponent<ComponentAuthority>(entity);

                entityManager.RemoveComponent<Improbable.Gdk.Health.HealthRegenComponent.Component>(entity);
            }

            private void ApplyUpdate(in ComponentUpdateReceived<Update> update, ComponentDataFromEntity<Component> dataFromEntity)
            {
                workerSystem.TryGetEntity(update.EntityId, out var entity);
                if (!dataFromEntity.Exists(entity))
                {
                    return;
                }

                var data = dataFromEntity[entity];

                if (update.Update.DamagedRecently.HasValue)
                {
                    data.DamagedRecently = update.Update.DamagedRecently.Value;
                }

                if (update.Update.RegenCooldownTimer.HasValue)
                {
                    data.RegenCooldownTimer = update.Update.RegenCooldownTimer.Value;
                }

                if (update.Update.CooldownSyncInterval.HasValue)
                {
                    data.CooldownSyncInterval = update.Update.CooldownSyncInterval.Value;
                }

                if (update.Update.RegenPauseTime.HasValue)
                {
                    data.RegenPauseTime = update.Update.RegenPauseTime.Value;
                }

                if (update.Update.RegenInterval.HasValue)
                {
                    data.RegenInterval = update.Update.RegenInterval.Value;
                }

                if (update.Update.RegenAmount.HasValue)
                {
                    data.RegenAmount = update.Update.RegenAmount.Value;
                }

                data.MarkDataClean();
                dataFromEntity[entity] = data;
            }

            private void SetAuthority(EntityId entityId, Authority authority)
            {
                switch (authority)
                {
                    case Authority.NotAuthoritative:
                    {
                        workerSystem.TryGetEntity(entityId, out var entity);
                        entityManager.SetSharedComponentData(entity, ComponentAuthority.NotAuthoritative);
                        break;
                    }
                    case Authority.Authoritative:
                    {
                        workerSystem.TryGetEntity(entityId, out var entity);
                        entityManager.SetSharedComponentData(entity, ComponentAuthority.Authoritative);
                        break;
                    }
                    case Authority.AuthorityLossImminent:
                        break;
                }
            }
        }
    }
}
