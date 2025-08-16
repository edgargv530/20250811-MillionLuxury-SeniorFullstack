import { useState } from 'react';
import Header from './components/Header'
import Menu from './components/Menu'
import Footer from './components/Footer';
import Owners from './components/Owners';
import Welcome from './components/Welcome';

function App() {
	const [view, setView] = useState('welcome');

	const handleMenuSelect = (option) => {
		setView(option);
	};

	return (
		<div className="container-fluid">
			<Header />

			{/* Área de trabajo */}
			<div className="row">
				{/* Menú lateral Izq. */}
				<Menu onSelect={handleMenuSelect} />

				{/* Cuerpo y carga de los componentes */}
				<main className="col ps-md-2 pt-2">
					<div className="row">
						<div className="col-12">
							{view === 'welcome' && <Welcome />}
							{view === 'owners' && <Owners />}
						</div>
					</div>
				</main>
			</div>

			<Footer />
		</div>
	);
}

export default App