import { useState } from 'react';
import MainLayout from './components/templates/MainLayout';
import Owners from './features/owners/Owners';
import Welcome from './features/welcome/Welcome';
import Properties from './features/properties/Properties';

function App() {
	const [view, setView] = useState('welcome');

	const handleMenuSelect = (option) => {
		setView(option);
	};

	return (
		<MainLayout onChangeMenu={handleMenuSelect}>
			{view === 'welcome' && <Welcome />}
			{view === 'owners' && <Owners />}
			{view == 'properties' && <Properties />}
		</MainLayout>
	);
}

export default App