import Oidc from 'oidc-client';

var userManager = new Oidc.UserManager({
    authority: window.$OpenAdminSettings.authority.authority,
    client_id: window.$OpenAdminSettings.authority.client_id,
    redirect_uri: window.$OpenAdminSettings.baseUrl + '#/oidc-signin&',
	response_type: 'id_token token',
    scope: window.$OpenAdminSettings.authority.scope,
    post_logout_redirect_uri: window.$OpenAdminSettings.baseUrl,
	userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
});

export default userManager;