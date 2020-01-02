// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App';
import Vuetify from 'vuetify';
import axios from 'axios';
import UserManager from './security/userManager';

// router setup
import routes from './routes/routes';

import vField from '@/components/Field';
Vue.component('v-field', vField);

import vObjectEditor from '@/components/ObjectEditor';
Vue.component('v-object-editor', vObjectEditor);

import vObjectListEditor from '@/components/ObjectListEditor';
Vue.component('v-object-list-editor', vObjectListEditor);

// configure router
const router = new VueRouter({
	routes, // short for routes: routes
	linkExactActiveClass: 'nav-item active',
	//mode: 'history'
});

//router.beforeEach(async (to, from, next) => {
//	const app = router.app.$data || { isAuthenticated: false };
//	if (app.isAuthenticated) {
//		//already signed in, we can navigate anywhere
//		next();
//	} else if (to.matched.some(record => !record.meta.allowAnonymous)) {
//		//authentication is required. Trigger the sign in process, including the return URI
//		router.app.$Identity.authenticate(to.path).then(() => {
//			console.log(`authenticating a protected url:${to.path}`);
//			next();
//		});
//	} else {
//		//No auth required. We can navigate
//		next();
//	}
//});

Vue.use(VueRouter);
Vue.use(Vuetify);
Vue.prototype.$http = axios;

Vue.prototype.$OpenAdmin = {
	setMetadata(data) {
		Vue.prototype.$OpenAdmin.metadata = data;
	},
	metadata: {},
	settings: window.$OpenAdminSettings
};

const globalData = {
	identity: {
        get user() {
            console.log(this);
			return this.userManager.getUser();
		},
        get isAuthenticated() {
			return !!this.user;
		},
		userManager: UserManager
	}
};

const globalMethods = {
	async authenticate(returnPath) {
		if (!this.identity.isAuthenticated) {
			await this.signIn(returnPath);
		}
    },
    signIn(returnPath) {
		returnPath
			? this.identity.userManager.signinRedirect({ state: returnPath })
            : this.identity.userManager.signinRedirect();
	}
};

//Vue.prototype.$Identity = {
//	get isAuthenticated() {
//		const $this = Vue.prototype.$Identity;
//		return !!$this.user;
//	},
//	authenticate: async (returnPath) => {
//		const $this = Vue.prototype.$Identity;
//		if (!$this.isAuthenticated) {
//			await $this.signIn(returnPath);
//		}
//	},
//	_getUser: async () => {
//		const $this = Vue.prototype.$Identity;
//		try {
//			return await $this.userManager.getUser();
//		} catch (err) {
//			console.log(err);
//		}
//	},
//	signIn: async (returnPath) => {
//		const $this = Vue.prototype.$Identity;
//		returnPath
//			? $this.userManager.signinRedirect({ state: returnPath })
//			: $this.userManager.signinRedirect();
//	},
//	userManager: UserManager,
//	get user() {
//		const $this = Vue.prototype.$Identity;
//		return $this._getUser();
//	}
//};

/* eslint-disable no-new */
const vue = new Vue({
	el: '#admin-app',
	render: h => h(App),
    data: globalData,
	methods: globalMethods,
	router
});