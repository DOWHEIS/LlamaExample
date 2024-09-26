import sys
import torch
from transformers import pipeline
from huggingface_hub import login

try:
    # go to https://huggingface.co/meta-llama/Llama-3.2-3B and apply to use the model
    # make a read token in huggingface
    huggingface_token = "TOKEN"

    login(huggingface_token)

    pipe = pipeline(
        "text-generation", 
        model="meta-llama/Llama-3.2-3B", 
        torch_dtype=torch.bfloat16, 
        device_map="auto"
    )

    input_prompt = sys.argv[1] if len(sys.argv) > 1 else "Summarize the recent changes in Apple stock."

    result = pipe(input_prompt, max_length=600, truncation=True)
    generated_text = result[0]['generated_text'].replace(input_prompt.strip(), "").strip()
    print(generated_text)
except Exception as e:
    print(f"Error encountered: {e}")