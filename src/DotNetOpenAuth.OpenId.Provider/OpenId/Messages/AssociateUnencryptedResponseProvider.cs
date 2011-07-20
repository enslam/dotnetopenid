﻿//-----------------------------------------------------------------------
// <copyright file="AssociateUnencryptedResponseProvider.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.OpenId.Messages {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using DotNetOpenAuth.OpenId.Provider;

	internal class AssociateUnencryptedResponseProvider : AssociateUnencryptedResponse {
		/// <summary>
		/// Called to create the Association based on a request previously given by the Relying Party.
		/// </summary>
		/// <param name="request">The prior request for an association.</param>
		/// <param name="associationStore">The Provider's association store.</param>
		/// <param name="securitySettings">The security settings of the Provider.</param>
		/// <returns>
		/// The created association.
		/// </returns>
		/// <remarks>
		///   <para>The caller will update this message's
		///   <see cref="AssociateSuccessfulResponse.ExpiresIn"/> and
		///   <see cref="AssociateSuccessfulResponse.AssociationHandle"/>
		/// properties based on the <see cref="Association"/> returned by this method, but any other
		/// association type specific properties must be set by this method.</para>
		///   <para>The response message is updated to include the details of the created association by this method,
		/// but the resulting association is <i>not</i> added to the association store and must be done by the caller.</para>
		/// </remarks>
		protected override Association CreateAssociationAtProvider(AssociateRequest request, IProviderAssociationStore associationStore, ProviderSecuritySettings securitySettings) {
			Association association = HmacShaAssociation.Create(Protocol, this.AssociationType, AssociationRelyingPartyType.Smart, associationStore, securitySettings);
			this.MacKey = association.SecretKey;
			return association;
		}

	}
}