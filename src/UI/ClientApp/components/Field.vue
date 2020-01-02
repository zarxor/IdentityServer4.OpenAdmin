<template>
    <v-flex xs12>
        <v-select :items="prop.enum"
                  :label="model.name"
                  v-model="item[model.propertyName]"
                  attach
                  box
                  multiple
                  @change="$emit('change', true)"
                  :required="item.required"
                  :hint="model.description"
                  persistent-hint
                  :disabled="disabled"
                  v-if="model.type == 'enum'"></v-select>
        <v-text-field :label="model.name"
                      v-model="item[model.propertyName]"
                      box
                      :type="model.format"
                      @change="$emit('change', true)"
                      :required="item.required"
                      :rules="rules"
                      :hint="model.description"
                      persistent-hint
                      :disabled="disabled"
                      v-else-if="model.type == 'text-input'"></v-text-field>
        <v-switch :label="model.name"
                  box
                  :required="item.required"
                  @change="$emit('change', true)"
                  v-model="item[model.propertyName]"
                  :hint="model.description"
                  persistent-hint
                  :disabled="disabled"
                  v-else-if="model.type == 'checkbox'"></v-switch>
        <v-select :label="model.name"
                  v-model="item[model.propertyName]"
                  :items="$OpenAdmin.metadata[model.metadataName]"
                  item-value="id"
                  item-text="name"
                  attach
                  box
                  :required="item.required"
                  @change="$emit('change', true)"
                  :hint="model.description"
                  persistent-hint
                  :disabled="disabled"
                  v-else-if="model.type == 'select'"></v-select>
        <v-combobox :label="model.name"
                    v-model="item[model.propertyName]"
                    @change="$emit('change', true)"
                    box
                    multiple
                    :required="item.required"
                    :hint="model.description"
                    persistent-hint
                    :disabled="disabled"
                    v-else-if="model.type == 'combobox'">
            <template slot="selection" slot-scope="data">
                <v-chip :selected="data.selected"
                        close
                        class="info white--text"
                        @input="removeFromArray(item[model.propertyName],data.item)">
                    {{ data.item }}
                </v-chip>
            </template>
        </v-combobox>
        <v-object-list-editor :label="model.name"
                              :model="item[model.propertyName]"
                              :type="model.format"
                              :definitions="definitions"
                              :hint="model.description"
                              @change="$emit('change', true)"
                              :disabled="disabled"
                              v-else-if="model.type == 'array'"></v-object-list-editor>
        <v-object-editor :label="model.name"
                         keyType="string"
                         valueType="string"
                         :model="item[model.propertyName]"
                         :hint="model.description"
                         @change="$emit('change', true)"
                         :disabled="disabled"
                         v-else-if="model.type == 'object'"></v-object-editor>
        <div v-else-if="model.type == 'datetime-picker'">
            <v-menu :close-on-content-click="false"
                    v-model="showMenu"
                    :nudge-right="40"
                    lazy
                    transition="scale-transition"
                    offset-y
                    full-width
                    :disabled="disabled"
                    min-width="290px">
                <v-text-field slot="activator"
                              v-model="item[model.propertyName]"
                              :label="model.name"
                              prepend-icon="event"
                              :hint="model.description"
                              persistent-hint
                              box
                              :disabled="disabled"
                              readonly></v-text-field>
                <v-date-picker v-model="item[model.propertyName]"
                               @input="showMenu = false"></v-date-picker>
            </v-menu>
        </div>
        <v-card color="error" class="mb-3 lighten-3" v-else>
            <v-card-title class="headline"><strong>{{model.type}}</strong>&nbsp;has no editor</v-card-title>
            <v-card-text><code>{{model}}</code></v-card-text>
        </v-card>
        <!--<v-divider class="mb-3"></v-divider>-->
    </v-flex>
</template>
<style lang="scss">
</style>
<script>
    export default {
        name: 'v-field',
        created() {
            this.buildRules();
        },
        mounted() {
        },
        props: {
            model: {
                type: Object,
                required: true
            },
            item: {
                type: Object,
                required: true
            },
            definitions: {
                type: Object,
                required: false
            },
            disabled: {
                type: Boolean,
                required: false
            }
        },
        methods: {
            removeFromArray(array, item) {
                array.splice(array.indexOf(item), 1)
                array = [...array]
            },
            buildRules() {
                this.rules = [];
                if (this.model.required) {
                    this.rules.push(value => !!value ||  this.model.name + ' is required.')
                }
            }
        },
        watch: {
            item(value) {
                this.buildRules();
            }
        },
        data: () => ({
            rules: [],
            showMenu: false
        })
    };
</script>
