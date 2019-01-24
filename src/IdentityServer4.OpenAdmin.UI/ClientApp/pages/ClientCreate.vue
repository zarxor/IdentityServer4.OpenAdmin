<template>
    <v-form v-model="form.isValid">
        <v-container fluid>
            <h1 class="mb-3">Create a new client</h1>
            <v-stepper vertical v-model="currentStep">
                <v-stepper-step :complete="currentStep > 1" step="1">
                    General information
                </v-stepper-step>
                <v-stepper-content step="1">
                    <v-text-field label="Client Id"
                                  :rules="form.rules.clientId"
                                  counter="200"
                                  box
                                  @change="form.hasChanged = true"
                                  v-model="client.clientId"></v-text-field>

                    <v-text-field label="Name"
                                  :rules="form.rules.clientName"
                                  counter="200"
                                  box
                                  @change="form.hasChanged = true"
                                  v-model="client.clientName"></v-text-field>

                    <v-text-field label="Description"
                                  counter="200"
                                  maxlength="200"
                                  box
                                  @change="form.hasChanged = true"
                                  v-model="client.description"></v-text-field>

                    <v-btn color="primary" @click="currentStep += 1" :disabled="!form.isValid">Continue</v-btn>
                    <v-btn flat @click="$emit('cancel')">Cancel</v-btn>
                </v-stepper-content>

                <v-stepper-step :complete="currentStep > 2" step="2">
                    Select template
                </v-stepper-step>
                <v-stepper-content step="2">
                    <v-list>
                        <template v-for="(template, index) in templates">
                            <v-list-tile avatar @click="selectedTemplate = template">
                                <v-list-tile-avatar>
                                    <v-icon color="info" v-if="selectedTemplate.id == template.id">radio_button_checked</v-icon>
                                    <v-icon color="info" v-else>radio_button_unchecked</v-icon>
                                </v-list-tile-avatar>
                                <v-list-tile-content>
                                    <v-list-tile-title v-html="template.name" />
                                    <v-list-tile-sub-title v-html="template.description" />
                                </v-list-tile-content>
                            </v-list-tile>
                        </template>
                    </v-list>
                    <v-btn color="primary" @click="currentStep++">Continue</v-btn>
                    <v-btn flat @click="currentStep--">Back</v-btn>
                </v-stepper-content>

                <v-stepper-step :complete="currentStep > 3" step="3">
                    Review
                </v-stepper-step>
                <v-stepper-content step="3">
                    <div class="subheading">Client Id</div>
                    <div>{{client.clientId}}</div>

                    <div class="subheading">Name</div>
                    <div> {{client.clientName}}</div>

                    <div class="subheading">Template</div>
                    <div>{{selectedTemplate.name}}</div>

                    <v-btn color="primary"
                           :disabled="form.isPosting"
                           @click="createClient">Create</v-btn>
                    <v-btn flat @click="currentStep--">Back</v-btn>
                </v-stepper-content>
            </v-stepper>
        </v-container>
    </v-form>
</template>
<style lang="scss">
</style>
<script>

    export default {
        created() {
            this.client = this.$OpenAdmin.metadata.defaultClient;
            this.client.clientId = this.guid();
            this.client.clientName = '';

            this.selectedTemplate = this.templates.none;
        },
        methods: {
            createClient() {
                this.form.isPosting = true;
                this.$OpenAdmin
                    .resources
                    .clients
                    .post(Object.assign(this.client, this.selectedTemplate.client))
                    .then(response => {
                        this.form.isPosting = false;
                        this.$router.push({ name: 'Client', params: { clientId: response.data.clientId } });
                    }, response => {
                        this.form.isPosting = false;
                        console.log(response);
                    })
            },
            guid() {
                function s4() {
                    return Math.floor((1 + Math.random()) * 0x10000)
                        .toString(16)
                        .substring(1);
                }
                return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
            }
        },
        data: () => ({
            currentStep: 1,
            selectedTemplate: null,
            client: null,
            error: null,
            form: {
                isValid: false,
                isPosting: false,
                hasChanged: false,
                rules: {
                    clientName: [
                        v => !!v || 'Name is required',
                        v => v.length <= 200 || 'Name must be less than 200 characters'
                    ],
                    clientId: [
                        v => !!v || 'Client Id is required',
                        v => v.length <= 200 || 'Client Id must be less than 200 characters',
                        v => !new RegExp('/^[\w\-]*$/igm').test(v) || 'Client Id can only contains letters, digits and "-"'
                    ]
                    //DisallowGrantTypeCombination(GrantType.Implicit, GrantType.AuthorizationCode, grantTypes);
                    //DisallowGrantTypeCombination(GrantType.Implicit, GrantType.Hybrid, grantTypes);

                    //DisallowGrantTypeCombination(GrantType.AuthorizationCode, GrantType.Hybrid, grantTypes);
                }
            },
            templates: {
                none: {
                    id: 'default',
                    name: "None",
                    description: "",
                    client: {
                        enabled: true
                    }
                },
                hybrid: {
                    id: 'hybrid',
                    name: "Hybrid",
                    description: "Create a basic hybrid client",
                    client: {
                        enabled: true,
                        allowedGrantTypes: [
                            'hybrid'
                        ]
                    }
                }
            }
        })
    };
</script>
