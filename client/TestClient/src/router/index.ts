import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: '/',
			name: 'home',
			component: () => import('../views/SignIn.vue'),
		},
		{
			path: '/events',
			name: 'events',
			component: () => import('@/views/EventListView.vue'),
		},
		{
			path: '/matches/{eventId}',
			name: 'matches',
			component: () => import('@/views/MatchListView.vue'),
		},
		{
			path: '/match/prediction',
			name: 'prediction',
			component: () => import('@/views/PredictionView.vue'),
		},
	],
})

export default router
