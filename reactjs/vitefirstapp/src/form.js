import react from 'react'

export default function Form(){

    const login = data => {
        console.log(data)
    }

    return(
        <div>
            <form onSubmit={handleSubmit(login)}>
                <input {...register('username')}/>
                <input {...register('password')}/>
                <button>login</button>

            </form>
        </div>
    )
}