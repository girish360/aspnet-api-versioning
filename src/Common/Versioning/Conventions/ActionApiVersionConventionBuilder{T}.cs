﻿#if WEBAPI
namespace Microsoft.Web.Http.Versioning.Conventions
#else
namespace Microsoft.AspNetCore.Mvc.Versioning.Conventions
#endif
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using static System.ComponentModel.EditorBrowsableState;

    /// <summary>
    /// Represents a builder for API versions applied to a controller action.
    /// </summary>
#pragma warning disable SA1619 // Generic type parameters should be documented partial class; false positive
    public partial class ActionApiVersionConventionBuilder<T> : ActionApiVersionConventionBuilderBase, IApiVersionConventionBuilder
#pragma warning restore SA1619
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionApiVersionConventionBuilder{T}"/> class.
        /// </summary>
        /// <param name="controllerBuilder">The <see cref="ControllerApiVersionConventionBuilder{T}">controller builder</see>
        /// the action builder belongs to.</param>
        public ActionApiVersionConventionBuilder( ControllerApiVersionConventionBuilder<T> controllerBuilder )
        {
            Arg.NotNull( controllerBuilder, nameof( controllerBuilder ) );
            ControllerBuilder = controllerBuilder;
        }

        /// <summary>
        /// Gets the controller builder the action builder belongs to.
        /// </summary>
        /// <value>The associated <see cref="ControllerApiVersionConventionBuilder{T}"/>.</value>
        protected ControllerApiVersionConventionBuilder<T> ControllerBuilder { get; }

        /// <summary>
        /// Gets or creates the convention builder for the specified controller action method.
        /// </summary>
        /// <param name="actionMethod">The <see cref="MethodInfo">method</see> representing the controller action.</param>
        /// <returns>A new or existing <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        [EditorBrowsable( Never )]
        public virtual ActionApiVersionConventionBuilder<T> Action( MethodInfo actionMethod ) => ControllerBuilder.Action( actionMethod );

        /// <summary>
        /// Maps the specified API version to the configured controller action.
        /// </summary>
        /// <param name="apiVersion">The <see cref="ApiVersion">API version</see> to map to the action.</param>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> MapToApiVersion( ApiVersion apiVersion )
        {
            Arg.NotNull( apiVersion, nameof( apiVersion ) );
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            MappedVersions.Add( apiVersion );
            return this;
        }

        /// <summary>
        /// Indicates that the action is API version-neutral.
        /// </summary>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> IsApiVersionNeutral()
        {
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            VersionNeutral = true;
            return this;
        }

        /// <summary>
        /// Indicates that the specified API version is supported by the configured action.
        /// </summary>
        /// <param name="apiVersion">The supported <see cref="ApiVersion">API version</see> implemented by the action.</param>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> HasApiVersion( ApiVersion apiVersion )
        {
            Arg.NotNull( apiVersion, nameof( apiVersion ) );
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            SupportedVersions.Add( apiVersion );
            return this;
        }

        /// <summary>
        /// Indicates that the specified API version is deprecated by the configured action.
        /// </summary>
        /// <param name="apiVersion">The deprecated <see cref="ApiVersion">API version</see> implemented by the action.</param>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> HasDeprecatedApiVersion( ApiVersion apiVersion )
        {
            Arg.NotNull( apiVersion, nameof( apiVersion ) );
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            DeprecatedVersions.Add( apiVersion );
            return this;
        }

        /// <summary>
        /// Indicates that the specified API version is advertised by the configured action.
        /// </summary>
        /// <param name="apiVersion">The advertised <see cref="ApiVersion">API version</see> not directly implemented by the action.</param>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> AdvertisesApiVersion( ApiVersion apiVersion )
        {
            Arg.NotNull( apiVersion, nameof( apiVersion ) );
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            AdvertisedVersions.Add( apiVersion );
            return this;
        }

        /// <summary>
        /// Indicates that the specified API version is advertised and deprecated by the configured action.
        /// </summary>
        /// <param name="apiVersion">The advertised, but deprecated <see cref="ApiVersion">API version</see> not directly implemented by the action.</param>
        /// <returns>The original <see cref="ActionApiVersionConventionBuilder{T}"/>.</returns>
        public virtual ActionApiVersionConventionBuilder<T> AdvertisesDeprecatedApiVersion( ApiVersion apiVersion )
        {
            Arg.NotNull( apiVersion, nameof( apiVersion ) );
            Contract.Ensures( Contract.Result<ActionApiVersionConventionBuilder<T>>() != null );

            DeprecatedAdvertisedVersions.Add( apiVersion );
            return this;
        }

        void IApiVersionConventionBuilder.IsApiVersionNeutral() => IsApiVersionNeutral();

        void IApiVersionConventionBuilder.HasApiVersion( ApiVersion apiVersion ) => HasApiVersion( apiVersion );

        void IApiVersionConventionBuilder.HasDeprecatedApiVersion( ApiVersion apiVersion ) => HasDeprecatedApiVersion( apiVersion );

        void IApiVersionConventionBuilder.AdvertisesApiVersion( ApiVersion apiVersion ) => AdvertisesApiVersion( apiVersion );

        void IApiVersionConventionBuilder.AdvertisesDeprecatedApiVersion( ApiVersion apiVersion ) => AdvertisesDeprecatedApiVersion( apiVersion );
    }
}