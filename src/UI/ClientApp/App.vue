<template>
    <authenticate v-if="!$root.identity.isAuthenticated"></authenticate>
    <v-app v-else-if="loadingMetadata || loadingRoutes">
        <v-container>
            <v-layout align-center
                      justify-center
                      fill-height
                      column>
                <v-progress-circular :size="70"
                                     :width="7"
                                     color="primary"
                                     indeterminate></v-progress-circular>
                <div class="headline">Loading OpenAdmin</div>
            </v-layout>
        </v-container>
    </v-app>
    <router-view v-else></router-view>
</template>

<script>
    import 'vuetify/dist/vuetify.min.css'

    import layout from '@/pages/Layout/_Layout';
    import dashboard from '@/pages/Dashboard';
    import list from '@/pages/List';
    import editor from '@/pages/Editor';
    import authenticate from '@/pages/Authenticate';

    export default {
        created() {
            console.log(this);
            if (!this.$root.identity.isAuthenticated)
                this.$Identity.authenticate(this.$root._route.fullPath);

            console.log(window.$OpenAdminSettings);
            console.log(this.$OpenAdmin);

            this.$http
                .get(this.$OpenAdmin.settings.baseUrl + "/api/routes")
                .then(response => {
                    const routes = [
                        {
                            path: '/',
                            component: layout,
                            redirect: '/dashboard',
                            children: this.getRoutes(response.data)
                        },
                        {
                            path: '/',
                            component: layout,
                            redirect: '/signin-oidc'
                        }
                    ];

                    this.$OpenAdmin.settings.routes = response.data;
                    this.$router.addRoutes(routes);

                    this.loadingRoutes = false;
                }, response => {
                    //this.loadingMetadata = false;
                });

            this.$http
                .get(this.$OpenAdmin.settings.apiBaseUrl + "/metadata")
                .then(response => {
                    this.$OpenAdmin.setMetadata(response.data);
                    this.loadingMetadata = false;
                }, response => {
                    //this.loadingMetadata = false;
                });
        },
        components: {
            authenticate
        },
        data: () => ({
            loadingMetadata: true,
            loadingRoutes: true
        }),
        methods: {
            getRoutes(routeList) {
                const newRoutes = [];
                routeList.forEach((r) => {
                    let routeComponent;
                    switch (r.type) {
                        case 0:
                            routeComponent = dashboard;
                            break;
                        case 1:
                            routeComponent = list;
                            break;
                        case 2:
                            routeComponent = editor;
                            break;
                        default:
                            routeComponent = null;
                            break;
                    }
                    newRoutes.push({
                        path: r.path,
                        name: r.name,
                        component: routeComponent,
                        children: !!r.children ? getRoutes(r.children) : null,
                        props: (route) => (Object.assign(route.params, r.properties))
                    });
                });

                return newRoutes;
            }
        }
    };
</script>