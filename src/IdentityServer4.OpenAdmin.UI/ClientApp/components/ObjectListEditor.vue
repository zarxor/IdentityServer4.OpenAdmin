<template>
    <v-card class="grey lighten-4 mb-3">
        <v-card-title class="grey lighten-3">{{label}}</v-card-title>
        <v-btn color="success"
               dark
               small
               absolute
               bottom
               left
               fab
               @click="dialog.isActive = true">
            <v-icon>add</v-icon>
        </v-btn>

        <v-data-table :headers="headers"
                      :items="model"
                      hide-actions>

            <template slot="items" slot-scope="props">
                <template v-for="(fieldGroup, fieldGroupIndex) in fieldGroups">
                    <template v-for="(fieldModel) in fieldGroup.fields">
                        <td v-if="fieldModel.type == 'checkbox'">
                            <v-icon v-if="props.item[fieldModel.propertyName]" color="primary">check_box</v-icon>
                            <v-icon v-else>check_box_outline_blank</v-icon>
                        </td>
                        <td v-else>{{props.item[fieldModel.propertyName]}}</td>
                    </template>
                </template>
                <td>
                    <v-icon small
                            class="mr-2"
                            @click="editRow(props.index)">
                        edit
                    </v-icon>
                    <v-icon small
                            @click="removeRow(props.index)">
                        delete
                    </v-icon>
                </td>
            </template>
        </v-data-table>
        <v-dialog v-model="dialog.isActive" max-width="500">
            <v-card>
                <v-card-text>
                    <template v-for="(fieldGroup, fieldGroupIndex) in fieldGroups">
                        <template v-for="(fieldModel) in fieldGroup.fields">
                            <v-field :model="fieldModel"
                                     :item="dialog.item"
                                     :definitions="definitions" />
                        </template>
                    </template>
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-btn color="error"
                           flat
                           @click="dialogClose">
                        <v-icon>cancel</v-icon> Cancel
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-btn color="primary"
                           default
                           @click="dialogSave">
                        <v-icon>save</v-icon> Save
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-card>
</template>
<style lang="scss">
</style>
<script>

    export default {
        name: 'v-object-list-editor',
        created() {
            this.getFieldGroups();
        },
        mounted() {
        },
        props: {
            label: {
                type: String
            },
            model: {
                type: Array,
                required: false
            },
            type: {
                type: String,
                required: true
            },
            definitions: {
                type: Object,
                required: false
            }
        },
        methods: {
            getFieldGroups() {
                if (this.type.startsWith("#")) {
                    var definition = this.type.split('/').pop();

                    this.fieldGroups = this.definitions[definition];
                    this.headers = [].concat.apply([], this.fieldGroups.map(g => g.fields.map(f => {
                        return {
                            text: f.name,
                            value: f.propertyName
                        }
                    })));
                    this.headers.push({
                        text: 'Actions',
                        value: this.headers[0].value,
                        sortable: false
                    });
                }
            },
            editRow(key) {
                this.dialog.key = key;
                this.dialog.item = JSON.parse(JSON.stringify(this.model[key]));
                this.dialog.isActive = true;

                console.log("Edit", this.dialog.item);
            },
            dialogSave() {
                this.addRow(this.dialog.key, this.dialog.item);
                this.dialogClose();

                console.log("ObjectListEdit.model", this.model);
            },
            dialogClose() {
                this.dialog.key = null;
                this.dialog.item = {};
                this.dialog.isActive = false;
            },
            addRow(key, value) {
                if (key != null)
                    this.model[key] = value;
                else
                    this.model.push(value);

                this.$forceUpdate();
                this.$emit('change', true);
            },
            removeRow(key) {
                if (confirm('Remove value?')) {
                    delete this.model[key];
                    this.$forceUpdate();
                    this.$emit('change', true);
                }
            }
        },
        data() {
            return {
                fieldGroups: [],
                headers: [
                    {
                        text: 'test'
                    }
                ],
                dialog: {
                    isActive: false,
                    key: null,
                    item: {}
                }
            }
        }
    };
</script>
