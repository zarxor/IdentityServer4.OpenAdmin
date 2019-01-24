<template>
    <v-toolbar :clipped-left="$vuetify.breakpoint.lgAndUp"
               color="blue darken-3"
               dark
               app
               fixed>
        <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
            <v-toolbar-side-icon @click.stop="toggleDrawer"></v-toolbar-side-icon>
            <span class="hidden-sm-and-down">OpenAdmin</span>
        </v-toolbar-title>
        <v-text-field flat
                      solo-inverted
                      hide-details
                      prepend-inner-icon="search"
                      label="Search"
                      class="hidden-sm-and-down"></v-text-field>
        <v-spacer></v-spacer>
        <v-btn icon>
            <v-icon>apps</v-icon>
        </v-btn>
        <v-btn icon>
            <v-icon>notifications</v-icon>
        </v-btn>
        <v-btn icon large>
            <v-avatar size="32px" tile>
                <img src="https://cdn.vuetifyjs.com/images/logos/logo.svg"
                     alt="Vuetify">
            </v-avatar>
        </v-btn>
    </v-toolbar>
</template>

<script>
    export default {
        props: {
            drawer: Boolean
        },
        methods: {
            toggleDrawer: function () {
                this.$emit('update:drawer', !this.drawer);
            }
        },
        data: () => ({
            items: [
                { icon: 'contacts', text: 'Clients' },
                { icon: 'history', text: 'Frequently contacted' },
                { icon: 'content_copy', text: 'Duplicates' },
                {
                    icon: 'keyboard_arrow_up',
                    'icon-alt': 'keyboard_arrow_down',
                    text: 'Labels',
                    model: true,
                    children: [
                        { icon: 'add', text: 'Create label' }
                    ]
                },
                {
                    icon: 'keyboard_arrow_up',
                    'icon-alt': 'keyboard_arrow_down',
                    text: 'More',
                    model: false,
                    children: [
                        { text: 'Import' },
                        { text: 'Export' },
                        { text: 'Print' },
                        { text: 'Undo changes' },
                        { text: 'Other contacts' }
                    ]
                },
                { icon: 'settings', text: 'Settings' },
                { icon: 'chat_bubble', text: 'Send feedback' },
                { icon: 'help', text: 'Help' },
                { icon: 'phonelink', text: 'App downloads' },
                { icon: 'keyboard', text: 'Go to the old version' }
            ]
        })
    };
</script>

<style lang="css">
</style>
