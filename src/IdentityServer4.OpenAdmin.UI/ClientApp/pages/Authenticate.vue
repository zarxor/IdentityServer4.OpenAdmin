<template>
    <v-app id="inspire">
        <v-content class="grey lighten-4">
            TEST!
        </v-content>
    </v-app>
</template>

<script>
    export default {
        async created() {
            try {
                var result = await this.$root.identity.userManager.signinRedirectCallback();
                console.log(`SigninRedirectCallbackResult`, result)
                var returnToUrl = '/';
                if (result.state !== undefined) { returnToUrl = result.state;}
                this.$router.push({ path: returnToUrl });
            } catch (e) {
                console.log(`Exception`, e);
                console.log(`Auth to: ${this.$root._route.fullPath}`);
                if (this.$root._route.fullPath == "/")
                    this.$root.authenticate(this.$root._route.fullPath);
                //this.$router.push({ name: 'Unauthorized' });
            }
        },
        components: {
        },
        data: () => ({
        }),
        props: {
        }
    }
</script>