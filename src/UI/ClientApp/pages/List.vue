<template>
    <clientCreate v-if="create" @cancel="create = false"></clientCreate>
    <v-container fluid v-else>
        <v-btn fab
               fixed
               bottom
               right
               color="success"
               @click="create = true">
            <v-icon>add</v-icon>
        </v-btn>
        <h1 class="mb-2">{{listOptions.header}}</h1>
        <v-card>
            <v-progress-linear :active="loadingItems"
                               v-if="loadingItems"
                               class="ma-0"
                               indeterminate></v-progress-linear>
            <v-list two-line>
                <template v-for="(item, index) in items">
                    <v-list-tile :to="listOptions.itemsUrl.replace('[id]', item[listOptions.itemKeys.id])">
                        <v-list-tile-avatar>
                            <img v-if="item.logoUri" :src="item.logoUri" />
                            <v-icon color="grey lighten4" v-else>image</v-icon>
                        </v-list-tile-avatar>
                        <v-list-tile-content>
                            <v-list-tile-title v-html="item[listOptions.itemKeys.title]" />
                            <v-list-tile-sub-title v-html="item[listOptions.itemKeys.subtitle]" />
                        </v-list-tile-content>
                        <v-list-tile-action>
                            <v-list-tile-action-text>{{ item[listOptions.itemKeys.id] }}</v-list-tile-action-text>
                        </v-list-tile-action>
                    </v-list-tile>

                    <v-divider v-if="index + 1 < items.length"
                               :key="index"></v-divider>
                </template>
            </v-list>
        </v-card>
    </v-container>
</template>
<style lang="scss">
</style>
<script>
    import clientCreate from '@/pages/ClientCreate';

    export default {
        mounted() {
            this.getItems();
            this.$watch("listOptions.url", this.getItems);
        },
        components: {
            clientCreate
        },
        props: {
            listOptions: Object
        },
        methods: {
            getItems() {
                this.loadingItems = true;
                this.items = [];
                this.$http
                    .get(this.listOptions.url)
                    .then(response => {
                        this.items = response.data;
                        this.loadingItems = false;
                    });
            }
        },
        data: () => ({
            loadingItems: true,
            create: false,
            items: []
        })
    };
</script>
