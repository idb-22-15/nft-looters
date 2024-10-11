// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  telemetry: { enabled: false },
  modules: [
    '@nuxt/fonts',
    '@nuxtjs/tailwindcss',
    'shadcn-nuxt',
    '@nuxt/eslint',
  ],
  eslint: {
    config: {
      stylistic: true,
    },
  },

  imports: {
    dirs: [],
  },
  components: { dirs: [] }
})
