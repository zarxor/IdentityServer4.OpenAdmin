<template>
    <v-app v-if="loadingMetadata || loadingRoutes">
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

    export default {
        created() {
            console.log(this.$OpenAdmin);
            console.log(window.$OpenAdminSettings);

            this.$http
                .get(this.$OpenAdmin.settings.baseUrl + "/api/routes")
                .then(response => {
                    const routes = [
                        {
                            path: '/',
                            component: layout,
                            redirect: '/dashboard',
                            children: this.getRoutes(response.data)
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