// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  telemetry: { enabled: false },
  modules: [
    '@nuxt/fonts',
    '@nuxt/eslint',
    '@nuxtjs/tailwindcss',
    'shadcn-nuxt',
  ],

  fonts: {
    defaults: {
      weights: [100, 200, 300, 400, 500, 600, 700, 800, 900],
    },
    // families: [
    //   { name: 'Onest', provider: 'google' },
    // ],
  },

  eslint: {
    config: {
      stylistic: true,
    },
  },

  imports: {
    dirs: [],
  },
  // components: { dirs: [] }
})
