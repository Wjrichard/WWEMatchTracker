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
			path: '/matches/:eventId',
			name: 'matches',
			component: () => import('@/views/MatchListView.vue'),
		},
		{
			path: '/match/prediction',
			name: 'prediction',
			component: () => import('@/views/PredictionView.vue'),
		},
		{
			path: '/events/:eventId/details',
			name: 'eventDetails',
			component: () => import('@/views/EventDetailsView.vue'),
		},
		{
			path: '/match/:matchId/details',
			name: 'matchDetails',
			component: () => import('@/views/MatchDetailsView.vue'),
		},
	],
})

export default router
