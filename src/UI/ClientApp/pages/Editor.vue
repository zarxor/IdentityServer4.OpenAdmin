<template>
    <v-container fluid v-if="item == null">
        <v-layout v-if="loadingItem"
                  align-center
                  justify-center
                  fill-height>
            <v-progress-circular :size="70"
                                 :width="7"
                                 color="primary"
                                 indeterminate></v-progress-circular>
        </v-layout>

        <v-alert :value="true"
                 type="error"
                 v-else-if="error && error.status == '404'"
                 class="display-1">
            {{editorOptions.dataType}} not found
        </v-alert>
        <v-layout v-else-if="error"
                  class="display-4"
                  align-center
                  justify-center
                  fill-height>
            {{error.status}}
        </v-layout>
    </v-container>
    <div v-else>
        <v-snackbar v-model="snackbar.enabled">
            {{ snackbar.text }}
            <v-btn color="pink"
                   flat
                   @click="snackbar.enabled = false">
                Close
            </v-btn>
        </v-snackbar>

        <v-layout wrap class="secondary white--text">
            <v-flex xs12>
                <v-progress-linear :active="form.isPosting"
                                   class="ma-0"
                                   color="green lighten-3"
                                   height="4"
                                   indeterminate></v-progress-linear>
            </v-flex>
            <v-layout align-start
                      column
                      justify-end
                      pa-3>
                <h3 class="headline">{{item[editorOptions.itemKeys.title]}}</h3>
                <span class="grey--text text--lighten-1">{{item[editorOptions.itemKeys.id]}}</span>
            </v-layout>
        </v-layout>

        <v-form v-model="form.isValid" ref="itemform">
            <v-btn v-on:click="saveItem"
                   :disabled="!form.isValid || form.isPosting || !form.hasChanged"
                   fab
                   fixed
                   bottom
                   right
                   color="success">
                <v-progress-circular v-if="form.isPosting"
                                     size="20"
                                     indeterminate></v-progress-circular>
                <v-icon v-else>save</v-icon>
            </v-btn>
            <!--<v-btn v-on:click="resetClient"
               :disabled="!form.hasChanged"
               fab
               fixed
               bottom
               left
               color="error">
                <v-icon>undo</v-icon>
            </v-btn>-->
            <v-tabs v-model="activeTab"
                    dark
                    slider-color="primary">

                <template v-for="(fieldGroup, fieldGroupIndex) in editorOptions.fieldGroups">
                    <v-tab :key="fieldGroupIndex" ripple>
                        {{fieldGroup.name}}
                    </v-tab>
                    <v-tab-item :key="fieldGroupIndex">
                        <v-container fluid>
                            <v-card>
                                <v-card-text>
                                    <v-layout wrap>
                                        <template v-for="(fieldModel) in fieldGroup.fields">
                                            <v-field :model="fieldModel"
                                                   :item="item"
                                                   :definitions="editorOptions.definitions"
                                                   @change="form.hasChanged = true" />
                                        </template>
                                    </v-layout>
                                </v-card-text>
                            </v-card>
                        </v-container>
                    </v-tab-item>
                </template>
            </v-tabs>
        </v-form>
    </div>
</template>
<style lang="scss">
</style>
<script>
    import jsonpatch from 'fast-json-patch';

    export default {
        mounted() {
            this.getItem();
            console.log(this.editorOptions);
        },
        props: {
            itemId: {
                type: String,
                required: true
            },
            editorOptions: Object
        },
        methods: {
            isPropOfType(prop, type) {
                return prop.type == type || Array.isArray(prop.type) && prop.type[0] == type;
            },
            getItem() {
                var url = this.editorOptions.url.replace("[id]", this.itemId);

                this.$http
                    .get(url)
                    .then(response => {
                        this.item = response.data;
                        this.itemObserver = jsonpatch.observe(this.item);

                        this.form.hasChanged = false;

                        this.loadingItem = false;
                    }, response => {
                        this.loadingItem = false;
                        this.error = {
                            status: response.status,
                            statusText: response.statusText
                        };
                    });
            },
            saveItem() {
                var url = this.editorOptions.url.replace("[id]", this.itemId);
                this.form.isPosting = true;
                var patchData = jsonpatch.generate(this.itemObserver);

                this.$http
                    .patch(url, patchData)
                    .then(response => {
                        this.item = response.data;
                        this.itemObserver = jsonpatch.observe(this.item);

                        this.snackbar.text = "Saved successfully!"
                        this.snackbar.enabled = true;

                        this.form.hasChanged = false;
                        this.form.isPosting = false;
                    }, response => {
                        this.snackbar.text = "Error saving: " + response.status;
                        this.snackbar.enabled = true;

                        this.form.isPosting = false;
                    });
            },
            resetClient() {
                this.$refs.itemform.reset();
            }
        },
        data: () => ({
            activeTab: null,
            item: null,
            itemObserver: null,
            loadingItem: true,
            error: null,
            snackbar: {
                enabled: false,
                timeout: 6000,
                text: ''
            },
            form: {
                isValid: false,
                isPosting: false,
                hasChanged: false
            }
        })
    };
</script>
