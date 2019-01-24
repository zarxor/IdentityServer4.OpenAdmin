// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App';
import Vuetify from 'vuetify';
import axios from 'axios';

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
    linkExactActiveClass: 'nav-item active'
});

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

/* eslint-disable no-new */
const vue = new Vue({
    el: '#admin-app',
    render: h => h(App),
    router
});