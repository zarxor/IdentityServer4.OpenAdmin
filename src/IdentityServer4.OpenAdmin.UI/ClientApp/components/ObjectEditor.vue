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
        <template v-for="(value, key) in model">
            <v-container grid-list-md fluid>
                <v-layout row wrap>
                    <v-flex xs12 sm2 class="body-2">
                        {{key}}
                    </v-flex>
                    <v-flex xs11 sm9>
                        <v-text-field label="Value"
                                        v-model="model[key]"
                                        box
                                        :type="valueType"
                                        @change="$emit('change', true)"></v-text-field>
                    </v-flex>
                    <v-flex xs1>
                        <v-btn fab flat small @click="removeRow(key)"><v-icon color="error">delete</v-icon></v-btn>
                    </v-flex>
                </v-layout>
            </v-container>
        </template>
        <v-dialog v-model="dialog.isActive" max-width="500">
            <v-card>
                <v-card-text>

                    <v-flex xs12>
                        <v-text-field label="Key"
                                      v-model="dialog.key"
                                      box
                                      :rules="dialog.rules.key"
                                      :type="keyType"></v-text-field>
                    </v-flex>
                    <v-flex xs12>
                        <v-text-field label="Value"
                                      v-model="dialog.value"
                                      box
                                      :type="valueType"></v-text-field>
                    </v-flex>
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
        name: 'v-object-editor',

        created() {
        },
        mounted() {
        },
        props: {
            label: {
                type: String
            },
            keyType: {
                type: String,
                required: true
            },
            valueType: {
                type: String,
                required: true
            },
            valueRequired: {
                type: Boolean,
                required: false,
                default: false
            },
            model: {
                type: Object,
                required: false
            }
        },
        methods: {
            dialogSave() {
                this.addRow(this.dialog.key, this.dialog.value);
                this.dialogClose();
            },
            dialogClose() {
                this.dialog.key = '';
                this.dialog.value = '';
                this.dialog.isActive = false;
            },
            addRow(key, value) {
                this.model[key] = value;
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
                dialog: {
                    isActive: false,
                    key: '',
                    value: '',
                    rules: {
                        key: [
                            v => (this.model == null || !(v in this.model)) || 'Key already exists'
                        ]
                    }
                }
            }
        }
    };
</script>
