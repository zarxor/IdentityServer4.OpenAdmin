<template>
    <v-navigation-drawer :clipped="$vuetify.breakpoint.lgAndUp"
                         v-model="drawer"
                         fixed
                         app
                         :mini-variant="mini">
        <v-list>
            <v-list-tile v-if="mini" @click.stop="mini = !mini">
                <v-list-tile-action>
                    <v-icon>chevron_right</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>TEST</v-list-tile-title>
            </v-list-tile>
            <v-list-tile avatar tag="div">
                <v-list-tile-avatar>
                    <img src="https://randomuser.me/api/portraits/men/85.jpg">
                </v-list-tile-avatar>

                <v-list-tile-content>
                    <v-list-tile-title>My User</v-list-tile-title>
                </v-list-tile-content>

                <v-list-tile-action>
                    <v-btn icon @click.stop="mini = !mini">
                        <v-icon>chevron_left</v-icon>
                    </v-btn>
                </v-list-tile-action>
            </v-list-tile>
        </v-list>
        <v-divider></v-divider>
        <v-list subheader>
            <template v-for="(item, i) in items">
                <v-subheader v-if="i == 0 || i > 0 && item.menuGroup != items[i-1].menuGroup">{{item.menuGroup}}</v-subheader>
                <v-list-tile append :to="item.path">
                    <v-list-tile-action>
                        <v-icon v-if="item.icon">{{item.icon}}</v-icon>
                        <v-icon v-else>label</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-title>{{item.name}}</v-list-tile-title>
                </v-list-tile>
            </template>
        </v-list>

        <!--<v-list subheader>
            <v-subheader>Home</v-subheader>
            <v-list-tile append to="/dashboard">
                <v-list-tile-action>
                    <v-icon>dashboard</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Dashboard</v-list-tile-title>
            </v-list-tile>
        </v-list>
        <v-list subheader>
            <v-subheader>Clients and resources</v-subheader>
            <v-list-tile to="/clients">
                <v-list-tile-action>
                    <v-icon>web</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Clients</v-list-tile-title>
            </v-list-tile>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>label</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Identity resources</v-list-tile-title>
            </v-list-tile>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>label</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Api resources</v-list-tile-title>
            </v-list-tile>
            <v-list-tile slot="activator">
                <v-list-tile-content>
                    <v-list-tile-title>Users and roles</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>

        </v-list>
        <v-list subheader>
            <v-subheader>Users and roles</v-subheader>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>group</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Users</v-list-tile-title>
            </v-list-tile>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>person_outline</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Roles</v-list-tile-title>
            </v-list-tile>
        </v-list>
        <v-list subheader>
            <v-subheader>Scopes and claims</v-subheader>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>label</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Scopes</v-list-tile-title>
            </v-list-tile>
            <v-list-tile append>
                <v-list-tile-action>
                    <v-icon>label</v-icon>
                </v-list-tile-action>
                <v-list-tile-title>Claims</v-list-tile-title>
            </v-list-tile>
        </v-list>-->
    </v-navigation-drawer>
</template>

<script>
    export default {
        created() {
            this.items = this.$OpenAdmin.settings.routes.filter(r => r.showInMenu);
        },
        props: {
            drawer: Boolean
        },
        data: () => ({
            mini: null,
            items: []
        })
    };
</script>

<style lang="css">
</style>
