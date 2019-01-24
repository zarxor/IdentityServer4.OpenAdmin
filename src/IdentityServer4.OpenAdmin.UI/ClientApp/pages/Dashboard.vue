<template>
    <v-container grid-list-md text-xs-center>
        <v-layout row wrap>
            <template v-for="stat in stats">
                <v-flex xs12 sm3>
                    <v-card style="height: 100%" :color="stat.color">
                        <v-card-text style="height: 100%">
                            <v-layout fill-height>
                                <v-flex xs6>
                                    <h3 class="headline mb-0" :color="stat.secondaryColor">{{ stat.text }}</h3>
                                    <v-icon large>{{ stat.icon }}</v-icon>
                                </v-flex>
                                <v-flex xs6 align-self-center v-if="stat.maxValue">
                                    <v-progress-circular :rotate="-90"
                                                         :size="100"
                                                         :width="15"
                                                         :value="stat.value / stat.maxValue * 100"
                                                         :color="stat.secondaryColor">
                                        {{ stat.value }}
                                    </v-progress-circular>
                                </v-flex>
                                <v-flex xs6 v-else class="display-3">
                                    {{ stat.value }}
                                </v-flex>
                            </v-layout>
                        </v-card-text>
                    </v-card>
                </v-flex>

            </template>
        </v-layout>
    </v-container>
</template>
<style lang="scss">
</style>
<script>
    export default {
        data: () => ({
            stats: [
                {
                    text: 'Active users',
                    icon: 'group',
                    value: 104,
                    maxValue: 144,
                    color: 'green',
                    secondaryColor: 'green lighten-4'
                },
                {
                    text: 'Inactive users',
                    icon: 'group',
                    value: 40,
                    maxValue: 144,
                    color: 'orange',
                    secondaryColor: 'orange lighten-4'
                },
                {
                    text: 'Locked out users',
                    icon: 'lock',
                    value: 15,
                    color: 'red',
                    secondaryColor: 'red lighter-3'
                },
                {
                    text: 'Clients',
                    icon: 'list',
                    value: 5
                }
            ]
        })
    };
</script>
